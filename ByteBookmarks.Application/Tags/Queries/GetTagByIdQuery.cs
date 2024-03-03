namespace ByteBookmarks.Server.Controllers;

public class GetTagByIdQuery(int id) : IRequest<object?>
{
    public int Id { get; } = id;
}