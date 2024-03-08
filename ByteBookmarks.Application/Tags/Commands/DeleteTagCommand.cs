namespace ByteBookmarks.Application.Tags.Commands;

public class DeleteTagCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string? UserId { get; set; }
}