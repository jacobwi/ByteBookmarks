#region

#endregion

#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users.Queries;

public class GetUserByIdQuery(string userId) : IRequest<ClientUser>
{
    public string UserId { get; set; } = userId;
}

// Handlers