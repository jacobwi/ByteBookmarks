#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IBookmarkRepository
{
    Task<IEnumerable<Bookmark>> GetBookmarksByUserIdAsync(string userId);
    Task<IEnumerable<Bookmark>> GetBookmarksByUsernameAsync(string username);

    Task<Bookmark> GetBookmarkByIdAsync(int id);
    Task AddBookmarkAsync(Bookmark bookmark);
    Task DeleteBookmarkAsync(Bookmark bookmark);
    Task UpdateBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken);
    Task<Category> GetCategoryByIdAsync(int categoryId);
}