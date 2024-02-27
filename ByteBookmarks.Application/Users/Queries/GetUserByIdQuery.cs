#region

#endregion

namespace ByteBookmarks.Application.Admin.Queries;

public class GetUserByIdQuery : IRequest<ClientUser>
{
    public GetUserByIdQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}