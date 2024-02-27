#region

using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class UpdateUserRoleCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserRoleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        // 1. Authorization (Important!): Ensure the current user has permission for this action

        // 2. Retrieve the user
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException("User not found");

        // 3. Update the role
        // TODO: Fix role 
        user.Role = Role.Basic;


        // 4. Persist the change
        await userRepository.UpdateUserAsync(user);

        return Unit.Value;
    }
}