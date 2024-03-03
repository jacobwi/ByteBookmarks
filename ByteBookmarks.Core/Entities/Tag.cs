#region

#endregion

#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ByteBookmarks.Core.Entities;

public class Tag
{
    [Key] public int TagId { get; set; }
    public string Name { get; set; }

    // Foreign key for User
    public string UserId { get; set; }

    // Navigation property back to ApplicationUser
    public virtual ApplicationUser User { get; set; }

    // Navigation property for many-to-many relationship with Bookmark
    public virtual ICollection<TagBookmark> TagBookmarks { get; set; }
}