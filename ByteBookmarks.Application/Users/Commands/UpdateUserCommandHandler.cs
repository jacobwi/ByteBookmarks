#region

using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Authorization (Optional): Check if the user has permission to perform this update

        // 2. Retrieve the user
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException("User not found");

        // 3. Map properties from the request to the user entity
        user.Email = request.Email;
        // ... update other properties ...

        // 4. Persist the changes
        await userRepository.UpdateUserAsync(user);

        return Unit.Value;
    }
}