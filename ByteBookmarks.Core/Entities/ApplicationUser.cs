namespace ByteBookmarks.Core.Entities;

public class ApplicationUser
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string Email { get; set; }

    // ... other properties
    public Role Role { get; set; }

    public ICollection<Bookmark> Bookmarks { get; set; }
}