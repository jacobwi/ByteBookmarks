#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetUserTags(string userId);
    Task<IEnumerable<Tag>> GetUserTags(string userId, int page, int pageSize);

    Task<Tag> GetTag(int id, string? userId);
    Task<Tag> CreateTag(Tag tag);
    Task<bool> DeleteTag(int id, string? userId);
    Task<bool> UpdateTag(Tag tag);
}