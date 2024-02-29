#region

#endregion

namespace ByteBookmarks.Application.Admin.Queries;

public class GetUserByIdQuery(string userId) : IRequest<ClientUser>
{
    public string UserId { get; set; } = userId;
}