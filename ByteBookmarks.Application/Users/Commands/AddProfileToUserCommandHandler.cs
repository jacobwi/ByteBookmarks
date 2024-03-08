namespace ByteBookmarks.Application.Users.Commands;

public class AddProfileToUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddProfileToUserCommand, UserProfile>
{
    public async Task<UserProfile?> Handle(AddProfileToUserCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new UserProfile
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Avatar = request.Avatar
        };

        return await userRepository.CreateUserProfileAsync(userProfile);
    }
}