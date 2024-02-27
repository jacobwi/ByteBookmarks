#region

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class UpdateUserCommand : IRequest<Unit>
{
    public string UserId { get; set; }

    public string Email { get; set; }
    // ... other editable properties ...
}