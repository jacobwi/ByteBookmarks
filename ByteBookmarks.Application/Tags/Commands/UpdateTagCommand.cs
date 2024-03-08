namespace ByteBookmarks.Application.Tags.Commands;

public class UpdateTagCommand : IRequest<TagDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? UserId { get; set; }
}