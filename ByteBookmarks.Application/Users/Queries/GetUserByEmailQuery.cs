#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users.Queries;

public class GetUserByEmailQuery(string email) : IRequest<ClientUser>
{
    public string Email { get; set; } = email;
}