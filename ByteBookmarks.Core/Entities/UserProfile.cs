#region

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ByteBookmarks.Core.Entities;

public class UserProfile
{
    [Key] [ForeignKey("ApplicationUser")] public string UserId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public Image? Avatar { get; set; }

    // Navigation property back to ApplicationUser
    public virtual ApplicationUser ApplicationUser { get; set; }
}