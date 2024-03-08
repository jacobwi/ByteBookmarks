namespace ByteBookmarks.Application.Users.Commands;

public class DeleteProfileFromUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<DeleteProfileFromUserCommand, UserProfile>
{
    public async Task<UserProfile> Handle(DeleteProfileFromUserCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.DeleteUserProfileAsync(request.UserId);
    }
}