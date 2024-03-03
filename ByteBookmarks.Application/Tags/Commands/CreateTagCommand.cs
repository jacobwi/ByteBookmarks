#region

#endregion

namespace ByteBookmarks.Application.Tags.Commands;

public class CreateTagCommand : IRequest<TagDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
}