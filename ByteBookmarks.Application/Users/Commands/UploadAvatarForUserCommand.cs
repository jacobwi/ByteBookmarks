namespace ByteBookmarks.Application.Users.Commands;

public class UploadAvatarForUserCommand(string userId, IFormFile file) : IRequest<bool>
{
    public IFormFile File { get; } = file;
    public string UserId { get; } = userId;
}