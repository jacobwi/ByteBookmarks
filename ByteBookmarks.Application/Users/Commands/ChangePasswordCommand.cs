#region

using MediatR;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class ChangePasswordCommand : IRequest<Unit>
{
    public string UserId { get; set; } // ID of the user
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}