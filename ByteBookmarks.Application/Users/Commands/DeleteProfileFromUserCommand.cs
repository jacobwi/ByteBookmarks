namespace ByteBookmarks.Application.Users.Commands;

public class DeleteProfileFromUserCommand(string userId) : IRequest<UserProfile>
{
    public string UserId { get; set; } = userId;
}