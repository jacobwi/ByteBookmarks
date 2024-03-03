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
}