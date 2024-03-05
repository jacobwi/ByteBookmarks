#region

#endregion

#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class RegisterUserCommand : IRequest<AuthenticationResponse>
{
    public string Username { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }
    // Optionally include Role if you have role-based authorization        
    // public string Role { get; set; }         
}