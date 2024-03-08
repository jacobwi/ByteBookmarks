#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users.Queries;

public class GetUserByUsernameQuery(string username) : IRequest<ClientUser>
{
    public string Username { get; set; } = username;
}