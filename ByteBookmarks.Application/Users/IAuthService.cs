#region

#endregion

namespace ByteBookmarks.Application.Authentication;

public interface IAuthService
{
    Task<AuthenticationResponse> GenerateJwtToken(ApplicationUser user);
    Task<AuthenticationResponse> RegisterUser(RegisterUserCommand registerUserCommand);
    Task<AuthenticationResponse> LoginUser(LoginUserCommand loginUserCommand);
}