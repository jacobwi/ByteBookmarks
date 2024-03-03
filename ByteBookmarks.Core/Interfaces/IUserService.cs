#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IUserService
{
    // TODO: Implement methods for user management
    // Task<ApplicationUser> GetUserByIdAsync(string userId);
    // Task<ApplicationUser> GetUserByEmailAsync(string email);
    // Task<ApplicationUser> GetUserByUsernameAsync(string username);
    // Task<ApplicationUser> GetUserByRefreshTokenAsync(string refreshToken);

    Task<ApplicationUser> GetCurrentUserAsync();
    string? GetCurrentUserId();
}