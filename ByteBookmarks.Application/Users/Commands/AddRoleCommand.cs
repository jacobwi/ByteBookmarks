#region

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class AddRoleCommand(string userId, string roleName) : IRequest<Unit>
{
    public string UserId { get; set; } = userId;
    public string RoleName { get; set; } = roleName;
}