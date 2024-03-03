namespace ByteBookmarks.Application.Bookmarks.Commands;

public class DeleteBookmarkTagCommand : IRequest<bool>
{
    public int BookmarkId { get; set; }
    public int TagId { get; set; }
    public string UserId { get; set; }
}