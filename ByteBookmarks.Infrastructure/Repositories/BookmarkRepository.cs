namespace ByteBookmarks.Infrastructure.Repositories;

public class BookmarkRepository(DataContext context) : IBookmarkRepository
{
    public async Task<IEnumerable<Bookmark>> GetBookmarksByUserIdAsync(string? userId, int page = 0, int pageSize = 10)
    {
        IQueryable<Bookmark> query = context.Bookmarks // IQueryable<Bookmark> is expected here
            .Where(b => b.UserId == userId)
            .Include(b => b.TagBookmarks)
            .ThenInclude(tb => tb.Tag)
            .Include(b => b.Image); // Still IQueryable<Bookmark> after Includes

        if (pageSize > 0)
        {
            page = Math.Max(page, 0); // Ensure 'page' is not negative
            query = query.Skip(page * pageSize).Take(pageSize); // Still IQueryable<Bookmark>
        }

        // The result of ToListAsync() can be assigned to a List<Bookmark>, which is compatible with IEnumerable<Bookmark>
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>> GetBookmarksByUsernameAsync(string username, int page = 0,
        int pageSize = 0)
    {
        // Get bookmarks by username
        if (pageSize == 0)
            return await context.Bookmarks
                .Where(b => b.User.Username == username)
                .ToListAsync();
        return await context.Bookmarks
            .Where(b => b.User.Username == username)
            .Skip(page * pageSize)
            .Take(pageSize)
            .Include(b => b.TagBookmarks)
            .Include(i => i.Image)
            .ToListAsync();
    }

    public async Task<Bookmark> GetBookmarkByIdAsync(int id)
    {
        return await context.Bookmarks
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task AddBookmarkAsync(Bookmark? bookmark)
    {
        await context.Bookmarks.AddAsync(bookmark);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteBookmarkAsync(Bookmark? bookmark)
    {
        context.Bookmarks.Remove(bookmark);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task UpdateBookmarkAsync(Bookmark? bookmark, CancellationToken cancellationToken)
    {
        context.Bookmarks.Update(bookmark);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddTagToBookmarkAsync(Bookmark bookmark, Tag tag, CancellationToken cancellationToken)
    {
        await context.TagBookmarks.AddAsync(new TagBookmark
        {
            BookmarkId = bookmark.Id,
            TagId = tag.TagId
        }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}