namespace ByteBookmarks.Application.Tags.Queries;

public class GetTagByIdQuery(int id) : IRequest<object?>
{
    public int Id { get; } = id;
}