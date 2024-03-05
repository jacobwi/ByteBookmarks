namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksByUserIdQuery(string userId) : IRequest<List<BookmarkDto>>
{
    public string UserId { get; } = userId;
}