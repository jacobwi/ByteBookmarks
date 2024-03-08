namespace ByteBookmarks.Core.Entities;

public class Bookmark
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }
    public string? Description { get; set; }

    // Consider using a secure method for password storage if necessary
    public string? PasswordHash { get; set; }

    // Navigation properties
    public string UserId { get; set; } // Foreign Key

    // Navigation property back to ApplicationUser
    public ApplicationUser User { get; set; } // Navigation Property

    // Navigation properties for many-to-many relationships
    public virtual ICollection<TagBookmark> TagBookmarks { get; set; }
    public virtual ICollection<CategoryBookmark> CategoryBookmarks { get; set; }
    public int? ImageId { get; set; } // Foreign Key

    public virtual Image? Image { get; set; } // Consider if this should be nullable or not

    // Additional properties
    public bool IsPasswordProtected { get; set; }
    // Remove Password property if not needed or ensure it's securely handled
}