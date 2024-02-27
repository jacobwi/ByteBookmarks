#region

using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using ByteBookmarks.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ByteBookmarks.Infrastructure.Repositories;

public class BookmarkRepository(DataContext context) : IBookmarkRepository
{
    public async Task<Bookmark> GetBookmarkByIdAsync(int id)
    {
        return await context.Bookmarks
            .Include(b => b.BookmarkTags) // Example: Include related tags
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
        await context.SaveChangesAsync(cancellationToken);
    }
}