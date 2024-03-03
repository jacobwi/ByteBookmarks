namespace ByteBookmarks.Application.Tags.Queries;

public class GetTagsByUserIdQuery(string userId) : IRequest<IEnumerable<TagDto>>
{
    public string UserId { get; set; } = userId;
}