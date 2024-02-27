#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class CreateBookmarkCommand : IRequest<NewBookmarkDto>
{
    public string Title { get; set; }
    public string URL { get; set; }
    public string Description { get; set; }
    public bool IsPasswordProtected { get; set; }
    public string Password { get; set; }
    public string? UserId { get; set; }
    public IFormFile Image { get; set; }
}