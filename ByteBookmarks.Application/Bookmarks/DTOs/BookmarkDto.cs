#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.DTOs;

public class NewBookmarkDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }
    public string Description { get; set; }
    public bool IsPasswordProtected { get; set; } = false;

    public string Password { get; set; } = string.Empty;
    public IFormFile Image { get; set; }
}

public class BookmarkDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }
    public string Description { get; set; }
    public bool IsPasswordProtected { get; set; } = false;

    public string Password { get; set; } = string.Empty;
    public string Image { get; set; }

    public IEnumerable<Tag> Tags { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}