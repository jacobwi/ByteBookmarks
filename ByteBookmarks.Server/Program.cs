#region

using System.Text;
using ByteBookmarks.Application;
using ByteBookmarks.Core.Interfaces;
using ByteBookmarks.Infrastructure.Repositories;
using ByteBookmarks.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

#endregion


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
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
        Type = SecuritySchemeType.Http,
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

// Authentication Service 
builder.Services.AddScoped<IAuthService, AuthService>();

// Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false, // Adjust for your use case
            ValidateAudience = false // Adjust for your use case
        };
    });

// Add repositories
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

// Add Services
builder.Services.AddScoped<IImageStorageService, LocalImageStorageService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
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