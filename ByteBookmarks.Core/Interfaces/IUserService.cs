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

    // Is the user an admin?
    bool IsAdmin();

    // Is the user a moderator?
    bool IsModerator();

    // Is the user a regular user?
    bool IsUser();

    // Is the user a guest?
    bool IsGuest();


    Task<UserProfile?> GetUserProfileAsync(string userId);
    Task<UserProfile?> UpdateUserProfileAsync(UserProfile? userProfile);
    Task<UserProfile?> CreateUserProfileAsync(UserProfile? userProfile);
    Task<UserProfile> DeleteUserProfileAsync(string userId);
}