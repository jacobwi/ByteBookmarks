#region

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class UpdateUserRoleCommand : IRequest<Unit>
{
    public string UserId { get; set; }
    public string NewRole { get; set; }
}