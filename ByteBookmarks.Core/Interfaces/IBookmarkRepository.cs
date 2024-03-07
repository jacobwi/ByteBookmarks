#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IBookmarkRepository
{
    Task<IEnumerable<Bookmark>> GetBookmarksByUserIdAsync(string userId, int page = 0, int pageSize = 0);
    Task<IEnumerable<Bookmark>> GetBookmarksByUsernameAsync(string username, int page = 0, int pageSize = 0);

    Task<Bookmark?> GetBookmarkByIdAsync(int id);
    Task AddBookmarkAsync(Bookmark? bookmark);
    Task<bool> DeleteBookmarkAsync(Bookmark? bookmark);
    Task UpdateBookmarkAsync(Bookmark? bookmark, CancellationToken cancellationToken);
    Task AddTagToBookmarkAsync(Bookmark bookmark, Tag tag, CancellationToken cancellationToken);
}