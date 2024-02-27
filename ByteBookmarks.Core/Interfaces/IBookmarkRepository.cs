#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IBookmarkRepository
{
    Task<Bookmark> GetBookmarkByIdAsync(int id);
    Task AddBookmarkAsync(Bookmark bookmark);
    Task DeleteBookmarkAsync(Bookmark bookmark);
    Task UpdateBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken);
}