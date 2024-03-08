#region

#endregion

namespace ByteBookmarks.Infrastructure.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<ApplicationUser?> GetUserByIdAsync(string id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ApplicationUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
    {
        context.Users.Add(user);

        // Create a user profile for the new user
        var userProfile = new UserProfile
        {
            UserId = user.Id
        };
        context.UserProfiles.Add(userProfile);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(ApplicationUser user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUserByIdAsync(string id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user != null) context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(ApplicationUser user)
    {
        // Option 1: Automatic change tracking if your context is configured for it 
        await context.SaveChangesAsync();

        // Option 2: Explicitly mark the entity state as modified
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(ApplicationUser user, string newRole)
    {
        user.Role = user.Role;
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role)
    {
        // Assuming you have a way to associate roles with users 
        return await context.Users
            .Where(u => u.Role.ToString() == role)
            .ToListAsync();
    }

    public async Task<UserProfile?> GetUserProfileAsync(string userId)
    {
        return await context.UserProfiles
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<UserProfile?> UpdateUserProfileAsync(UserProfile? userProfile)
    {
        context.UserProfiles.Update(userProfile);
        await context.SaveChangesAsync();
        return userProfile;
    }

    public async Task<UserProfile?> CreateUserProfileAsync(UserProfile? userProfile)
    {
        context.UserProfiles.Add(userProfile);
        await context.SaveChangesAsync();
        return userProfile;
    }

    public async Task<UserProfile> DeleteUserProfileAsync(string userId)
    {
        var userProfile = await context.UserProfiles
            .FirstOrDefaultAsync(u => u.UserId == userId);
        if (userProfile != null) context.UserProfiles.Remove(userProfile);
        await context.SaveChangesAsync();
        return userProfile;
    }
}