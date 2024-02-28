#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksQuery(string userId) : IRequest<IEnumerable<BookmarkDto>>
{
    public string UserId { get; set; } = userId; // Or get directly from authentication context
}