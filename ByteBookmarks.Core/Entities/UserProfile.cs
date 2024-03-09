#region

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ByteBookmarks.Core.Entities;

public class UserProfile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Indicates the PK is not database-generated
    public string UserId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    [MaxLength(500)] public string? Bio { get; set; }

    public int? AvatarId { get; set; } // Made nullable in case a UserProfile might not have an Avatar

    [ForeignKey("AvatarId")] public virtual Image? Avatar { get; set; }

    // Navigation property back to ApplicationUser
    // The ForeignKey attribute here specifies which property is the foreign key to ApplicationUser
    [ForeignKey("UserId")] public virtual ApplicationUser ApplicationUser { get; set; }
}