namespace ByteBookmarks.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string UserId { get; set; } // User who owns this category
    public ApplicationUser User { get; set; }

    public ICollection<BookmarkCategory> BookmarkCategories { get; set; }
}