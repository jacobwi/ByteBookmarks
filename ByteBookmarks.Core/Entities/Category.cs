namespace ByteBookmarks.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; } // Foreign key to ApplicationUser

    // Navigation properties
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}