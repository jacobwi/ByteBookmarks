#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task<ApplicationUser?> GetUserByUsernameAsync(string username);
    Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
    Task DeleteUserByIdAsync(string id);

    Task DeleteUserAsync(ApplicationUser user);
    Task UpdateUserAsync(ApplicationUser user);
    Task UpdateUserRoleAsync(ApplicationUser user, string newRole);

    // Additional Methods for Enhanced Functionality
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role);

    Task<UserProfile?> GetUserProfileAsync(string userId);
    Task<UserProfile?> UpdateUserProfileAsync(UserProfile? userProfile);
    Task<UserProfile?> CreateUserProfileAsync(UserProfile? userProfile);
    Task<UserProfile> DeleteUserProfileAsync(string userId);
}