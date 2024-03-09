#region

using ByteBookmarks.Application.Users.DTOs;

#endregion

namespace ByteBookmarks.Application.Users.Queries;

public class GetUserProfileByIdQuery(string userId) : IRequest<UserProfileDto>
{
    public string UserId { get; set; } = userId;
}