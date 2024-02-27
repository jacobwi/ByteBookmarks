#region

using ByteBookmarks.Application.Users.Commands;
using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Authentication.Commands;

public class DeleteUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Authorization (Optional): Ensure the current user has permission for this action

        // May use either _authRepository or _adminService depending on where your deletion logic resides          
        await userRepository.DeleteUserByIdAsync(request.UserId);

        return Unit.Value; // Signifies successful completion
    }
}