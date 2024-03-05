namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksWithPaginationQuery(string userId, int page, int pageSize) : IRequest<List<BookmarkDto>>
{
    public string UserId { get; } = userId;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}