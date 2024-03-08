namespace ByteBookmarks.Application.Users.DTOs;

public class UserProfileDto
{
    public string UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    // This is a base64 string
    public string Avatar { get; set; }
}