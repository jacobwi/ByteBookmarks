#region

using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using ByteBookmarks.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

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
        context.Users.Remove(user);
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
}