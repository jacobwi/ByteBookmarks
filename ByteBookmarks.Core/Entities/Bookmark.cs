namespace ByteBookmarks.Core.Entities;

public class Bookmark
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public bool IsPasswordProtected { get; set; }
    public string Password { get; set; }

    public int ImageId { get; set; }
    public Image Image { get; set; } // Navigation property 

    public ICollection<BookmarkTag> BookmarkTags { get; set; }
    public ICollection<BookmarkCategory> BookmarkCategories { get; set; }
    public string Description { get; set; }
}