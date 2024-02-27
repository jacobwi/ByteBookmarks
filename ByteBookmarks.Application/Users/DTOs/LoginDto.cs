#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace ByteBookmarks.Application.Authentication;

public class LoginDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } // Or Email, if you allow login via email

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}