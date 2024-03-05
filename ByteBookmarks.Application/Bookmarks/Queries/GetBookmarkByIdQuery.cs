#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarkByIdQuery(int id, string? userId) : IRequest<BookmarkDto>
{
    public int Id { get; set; } = id;
    public string? UserId { get; set; } = userId;
}