#region

using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class AddRoleCommandHandler(IUserRepository userRepository) : IRequestHandler<AddRoleCommand, Unit>
{
    public async Task<Unit> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        // 1. Authorization (Important!): Ensure the current user has permission for this action

        // 2. Validate that the provided role exists (if applicable)

        // 3. Load the user
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException("User not found");

        // 4. Add the role (Implementation depends on your role management system)
        // ... Logic to add the role ...

        // 5. Persist changes 
        await userRepository.UpdateUserAsync(user);

        return Unit.Value; // Indicate successful operation
    }
}