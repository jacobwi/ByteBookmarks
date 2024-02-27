#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Application.Authentication;

public interface IAuthService
{
    Task<AuthenticationResponse> GenerateJwtToken(ApplicationUser user);
    Task<AuthenticationResponse> RegisterUser(SignupDto userDto);
    Task<AuthenticationResponse> LoginUser(LoginDto userDto);
}