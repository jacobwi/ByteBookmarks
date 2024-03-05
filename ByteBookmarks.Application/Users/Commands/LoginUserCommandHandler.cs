#region

#endregion

#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class LoginUserCommandHandler(IAuthService authService)
    : IRequestHandler<LoginUserCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginUser(request);
    }
}