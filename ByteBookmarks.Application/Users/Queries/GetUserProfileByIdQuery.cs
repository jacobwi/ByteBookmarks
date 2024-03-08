namespace ByteBookmarks.Application.Users.Queries;

public class GetUserProfileByIdQuery(string userId) : IRequest<UserProfile>
{
    public string UserId { get; set; } = userId;
}