#region

#endregion

namespace ByteBookmarks.Infrastructure.Repositories;

public class BookmarkRepository(DataContext context) : IBookmarkRepository
{
    public async Task<IEnumerable<Bookmark>> GetBookmarksByUserIdAsync(string userId)
    {
        return await context.Bookmarks
            .Where(b => b.UserId == userId)
            .Include(i => i.Image)
            .Include(t => t.Tags).Include(c => c.Categories) // Example: Include related tags
            .ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>> GetBookmarksByUsernameAsync(string username)
    {
        return await context.Bookmarks
            .Where(b => b.User.Username == username)
            .Include(i => i.Image)
            .Include(t => t.Tags).Include(c => c.Categories) // Example: Include related tags
            .ToListAsync();
    }

    public async Task<Bookmark> GetBookmarkByIdAsync(int id)
    {
        return await context.Bookmarks
            .Include(i => i.Image)
            .Include(t => t.Tags).Include(c => c.Categories) // Example: Include related tags
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddBookmarkAsync(Bookmark bookmark)
    {
        await context.Bookmarks.AddAsync(bookmark);
        await context.SaveChangesAsync();
    }

    public async Task DeleteBookmarkAsync(Bookmark bookmark)
    {
        context.Bookmarks.Remove(bookmark);
        await context.SaveChangesAsync();
    }

    public async Task UpdateBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken)
    {
        context.Bookmarks.Update(bookmark);
        await context.SaveChangesAsync(cancellationToken);
    }
}