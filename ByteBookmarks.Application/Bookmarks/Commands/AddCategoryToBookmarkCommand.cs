namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddCategoryToBookmarkCommand : IRequest<string>
{
    public int BookmarkId { get; set; }
    public string CategoryName { get; set; }
    public int CategoryId { get; set; }
}