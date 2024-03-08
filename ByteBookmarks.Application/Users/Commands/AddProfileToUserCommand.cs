namespace ByteBookmarks.Application.Users.Commands;

public class AddProfileToUserCommand : IRequest<UserProfile>
{
    public string UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Image? Avatar { get; set; }
}