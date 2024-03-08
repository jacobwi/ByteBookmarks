namespace ByteBookmarks.Core.Entities;

public class ApplicationUser
{
    public ApplicationUser()
    {
        Bookmarks = new HashSet<Bookmark>();
        Tags = new HashSet<Tag>();
        Categories = new HashSet<Category>();
    }

    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string Email { get; set; }

    // ... other properties
    public Role Role { get; set; }

    // One to Many / Lazy Loading
    public virtual ICollection<Bookmark> Bookmarks { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
    public virtual ICollection<Category> Categories { get; set; }

    // One to One / Eager Loading
    public virtual UserProfile Profile { get; set; }
}