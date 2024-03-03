namespace ByteBookmarks.Infrastructure.Repositories;

public class BookmarkRepository(DataContext context) : IBookmarkRepository
{
    public async Task<IEnumerable<Bookmark>> GetBookmarksByUserIdAsync(string userId)
    {
        // Get user bookmarks, boookmark images,  bookmarks categories and tags
        var bookmarks = context.Bookmarks
            .Include(i => i.Image)
            .Include(t => t.TagBookmarks).ThenInclude(t => t.Tag).Include(c => c.CategoryBookmarks)
            .Where(b => b.UserId == userId);

        return await bookmarks.ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>> GetBookmarksByUsernameAsync(string username)
    {
        return await context.Users.Where(u => u.Username == username).Include(u => u.Bookmarks)
            .SelectMany(u => u.Bookmarks).ToListAsync();
    }

    public async Task<Bookmark> GetBookmarkByIdAsync(int id)
    {
        return await context.Bookmarks.Include(i => i.Image).Include(t => t.TagBookmarks).ThenInclude(t => t.Tag)
            .Include(c => c.CategoryBookmarks).FirstOrDefaultAsync(b => b.Id == id);
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

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
    }
}