#region

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class DeleteUserCommand(string userId) : IRequest<Unit>
{
    public string UserId { get; set; } = userId;
}