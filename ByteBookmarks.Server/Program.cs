#region

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ByteBookmarks.Application;
using ByteBookmarks.Application.Users;
using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Exceptions;
using ByteBookmarks.Core.Interfaces;
using ByteBookmarks.Infrastructure.Repositories;
using ByteBookmarks.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

#endregion


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
});

builder.Services.AddEndpointsApiExplorer();


// Swagger

// Get sec scheme
var securitySchemeType = builder.Configuration["Jwt:Type"] switch
{
    "http" => SecuritySchemeType.Http,
    "apiKey" => SecuritySchemeType.ApiKey,
    "oauth2" => SecuritySchemeType.OAuth2,
    _ => SecuritySchemeType.Http // Default case
};
builder.Services.AddSwaggerGen(opt =>
{
    // Add info from configuration file
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["Swagger:Title"],
        Version = builder.Configuration["Swagger:Version"],
        Description = builder.Configuration["Swagger:Description"],
        Contact = new OpenApiContact
        {
            Name = builder.Configuration["Swagger:Contact:Name"],
            Email = builder.Configuration["Swagger:Contact:Email"],
            Url = new Uri(builder.Configuration["Swagger:Contact:Url"])
        },
        License = builder.Configuration["Swagger:License"] == null
            ? null
            : new OpenApiLicense
            {
                Name = builder.Configuration["Swagger:License:Name"],
                Url = new Uri(builder.Configuration["Swagger:License:Url"])
            }
    });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = securitySchemeType,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Database Context (if using EF Core)
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Authentication Service 
builder.Services.AddScoped<IAuthService, AuthService>();

// Authentication Configuration
// Get JWT preferences from configuration
var jwtConfig = builder.Configuration.GetSection("Jwt");

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = jwtConfig["ValidateAudience"] == "true",
            ValidateLifetime = jwtConfig["ValidateLifetime"] == "true",
            ValidateIssuerSigningKey = jwtConfig["ValidateIssuerSigningKey"] == "true",
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"])),
            ClockSkew = TimeSpan.FromMinutes(2),
            RoleClaimType = "role",
            NameClaimType = JwtRegisteredClaimNames.Sub
        };
    });

// Add repositories
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// Add Services
builder.Services.AddScoped<IImageStorageService, LocalImageStorageService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ImageService>();

// Add MeditorR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IAuthService).Assembly));
MappingConfiguration.Configure();

// Parse cors values from configuration then add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(builder.Configuration["CORS:PolicyName"],
        cb =>
        {
            if (bool.Parse(builder.Configuration["CORS:AllowAnyOrigin"]))
                cb.AllowAnyOrigin();
            else
                cb.WithOrigins(builder.Configuration["CORS:AllowedOrigins"].Split(','));

            if (bool.Parse(builder.Configuration["CORS:AllowAnyMethod"]))
                cb.AllowAnyMethod();
            else
                cb.WithMethods(builder.Configuration["CORS:AllowedMethods"].Split(','));

            if (bool.Parse(builder.Configuration["CORS:AllowAnyHeader"]))
                cb.AllowAnyHeader();
            else
                cb.WithHeaders(builder.Configuration["CORS:AllowedHeaders"].Split(','));

            if (bool.Parse(builder.Configuration["CORS:AllowCredentials"]) &&
                !bool.Parse(builder.Configuration["CORS:AllowAnyOrigin"]))
                cb.AllowCredentials();
            else
                cb.DisallowCredentials();
        });
});

var app = builder.Build();

// Configure global exception handler
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = context.Features.Get<IExceptionHandlerFeature>() switch
        {
            var feature when feature.Error is EntityNotFoundException => StatusCodes.Status404NotFound,
            var feature when feature.Error is NotAuthorizedException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = contextFeature.Error.Message
            }.ToString() ?? string.Empty);
    });
});


// Configure CORS policy
app.UseCors(builder.Configuration["CORS:PolicyName"]);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Always place Authentication before Authorization: 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();