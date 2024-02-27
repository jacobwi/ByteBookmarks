namespace ByteBookmarks.Core.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property for many-to-many 
    public ICollection<BookmarkTag> BookmarkTags { get; set; }
}