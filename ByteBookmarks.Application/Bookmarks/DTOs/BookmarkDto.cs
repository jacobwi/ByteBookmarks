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
    public bool IsPasswordProtected { get; set; }
    public int? ImageId { get; set; }

    // Related entities represented by their IDs or simplified DTOs
    public BookmarkImageDto Image { get; set; }
    public ICollection<BookmarkTagDto> Tags { get; set; }
    public ICollection<BookmarkCategoryDto> Categories { get; set; }
}