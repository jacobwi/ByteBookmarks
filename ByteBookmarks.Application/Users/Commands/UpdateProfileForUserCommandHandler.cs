namespace ByteBookmarks.Application.Users.Commands;

public class UpdateProfileForUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateProfileForUserCommand, UserProfile>
{
    public async Task<UserProfile?> Handle(UpdateProfileForUserCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new UserProfile
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Avatar = request.Avatar
        };

        return await userRepository.UpdateUserProfileAsync(userProfile);
    }
}