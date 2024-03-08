#region

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

#endregion

namespace ByteBookmarks.Infrastructure.Services;

public class UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : IUserService
{
    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var userId = GetCurrentUserId();
        if (userId == null) return null;

        return await userRepository.GetUserByIdAsync(userId);
    }

    public string? GetCurrentUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId");
        return userIdClaim;
    }

    public bool IsAdmin()
    {
        return httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
    }

    public bool IsModerator()
    {
        return httpContextAccessor.HttpContext?.User?.IsInRole("Moderator") ?? false;
    }

    public bool IsUser()
    {
        return httpContextAccessor.HttpContext?.User?.IsInRole("User") ?? false;
    }

    public bool IsGuest()
    {
        return httpContextAccessor.HttpContext?.User?.IsInRole("Guest") ?? false;
    }

    public async Task<UserProfile?> GetUserProfileAsync(string userId)
    {
        return await userRepository.GetUserProfileAsync(userId);
    }

    public async Task<UserProfile?> UpdateUserProfileAsync(UserProfile? userProfile)
    {
        return await userRepository.UpdateUserProfileAsync(userProfile);
    }

    public async Task<UserProfile?> CreateUserProfileAsync(UserProfile? userProfile)
    {
        return await userRepository.CreateUserProfileAsync(userProfile);
    }

    public async Task<UserProfile> DeleteUserProfileAsync(string userId)
    {
        return await userRepository.DeleteUserProfileAsync(userId);
    }
}