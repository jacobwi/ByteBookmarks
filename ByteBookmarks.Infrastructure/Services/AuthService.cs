#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ByteBookmarks.Application.Authentication;
using ByteBookmarks.Application.Users.Commands;
using ByteBookmarks.Core.Entities;
using ByteBookmarks.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AuthenticationResponse = ByteBookmarks.Application.Authentication.AuthenticationResponse;

#endregion

namespace ByteBookmarks.Infrastructure.Services;

public class AuthService(IConfiguration configuration, DataContext context) : IAuthService
{
    public async Task<AuthenticationResponse> GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); // Replace with how you retrieve your secret
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );

        // Key change:
        return await Task.FromResult(new AuthenticationResponse
        {
            JwtToken = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

    public async Task<AuthenticationResponse> RegisterUser(RegisterUserCommand userDto)
    {
        // Check for existing username
        if (await context.Users.AnyAsync(u => u.Username == userDto.Username))
            throw new Exception("Username already exists"); // Or return a more specific error

        // Hash the password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        var newUser = new ApplicationUser
        {
            Username = userDto.Username,
            Password = hashedPassword,
            Email = userDto.Email,
            Id = Guid.NewGuid().ToString(),
            Role = Role.Basic

        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        return await GenerateJwtToken(newUser);
    }

    public async Task<AuthenticationResponse> LoginUser(LoginUserCommand userDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            throw new Exception("Invalid username or password"); // Or a more specific error

        return
            await GenerateJwtToken(user);
    }

    // Helper for password verification (replace with your hashing mechanism)
    private bool VerifyPassword(string inputPassword, string storedPasswordHash)
    {
        // Implement your password hashing and verification logic here, 
        // e.g., using BCrypt or similar
        return inputPassword == storedPasswordHash;
    }
}