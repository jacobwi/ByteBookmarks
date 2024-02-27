#region

using ByteBookmarks.Application.Authentication;
using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using MediatR;
using AuthenticationResponse = ByteBookmarks.Application.Authentication.AuthenticationResponse;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class RegisterUserCommandHandler(IUserRepository userRepository, IAuthService authService)
    : IRequestHandler<RegisterUserCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Input Validation (Not shown for brevity)

        // 2. Check if the username is already in use
        if (await userRepository.GetUserByUsernameAsync(request.Username) != null)
            throw new Exception("Username already exists");

        // 3. Create the user object 
        var user = new ApplicationUser
        {
            Username = request.Username,
            Email = request.Email,
            // 4. Hash Password 
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            // ... Other properties 
        };

        ;

        // 5. Save the user
        var createdUser = await userRepository.CreateUserAsync(user);

        // 6. Generate and return JWT token 
        return await authService.GenerateJwtToken(createdUser);
    }
}