#region

#endregion

namespace ByteBookmarks.Application.Users.DTOs;

public class ClientUser
{
    public string Username { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}