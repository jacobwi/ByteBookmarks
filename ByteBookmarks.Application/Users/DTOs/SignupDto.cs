#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ByteBookmarks.Application.Authentication;

public class SignupDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
    public string Password { get; set; }
}