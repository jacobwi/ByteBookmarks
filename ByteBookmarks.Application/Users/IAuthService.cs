#region

#endregion

#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users;

public interface IAuthService
{
    Task<AuthenticationResponse> GenerateJwtToken(ApplicationUser user);
    Task<AuthenticationResponse> RegisterUser(RegisterUserCommand registerUserCommand);
    Task<AuthenticationResponse> LoginUser(LoginUserCommand loginUserCommand);
}