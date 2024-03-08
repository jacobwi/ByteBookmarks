namespace ByteBookmarks.Application.Users.Queries;

public class GetUserProfileByIdQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserProfileByIdQuery, UserProfile>
{
    public async Task<UserProfile?> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserProfileAsync(request.UserId);
    }
}