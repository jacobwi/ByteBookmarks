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
        return await authService.RegisterUser(request);
    }
}