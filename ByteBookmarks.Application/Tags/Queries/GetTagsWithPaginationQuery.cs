namespace ByteBookmarks.Application.Tags.Queries;

public class GetTagsWithPaginationQuery(string userId, int page, int pageSize) : IRequest<IEnumerable<Tag>>
{
    public string UserId { get; } = userId;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}