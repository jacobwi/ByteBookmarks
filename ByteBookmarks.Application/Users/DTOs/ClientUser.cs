#region

#endregion

namespace ByteBookmarks.Application.Authentication;

public class ClientUser
{
    public string Username { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}