#region

using ByteBookmarks.Application.Authentication;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class LoginUserCommand : IRequest<AuthenticationResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}