namespace ByteBookmarks.Core.Entities;

public class BookmarkTag
{
    public int Id { get; set; }
    public Bookmark Bookmark { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}