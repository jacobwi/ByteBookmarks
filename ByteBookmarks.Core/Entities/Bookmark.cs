namespace ByteBookmarks.Core.Entities;

public class Bookmark
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; }

    // Consider using a secure method for password storage if necessary
    public string PasswordHash { get; set; }

    // Navigation properties
    public virtual ApplicationUser User { get; set; }
    public virtual Image Image { get; set; } // Consider if this should be nullable or not
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    // Additional properties
    public bool IsPasswordProtected { get; set; }
    // Remove Password property if not needed or ensure it's securely handled

    // If the image is optional, make the foreign key nullable
    public int? ImageId { get; set; }
}