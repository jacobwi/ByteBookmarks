namespace ByteBookmarks.Core.Entities;

public class BookmarkCategory
{
    public int Id { get; set; }
    public Bookmark Bookmark { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}