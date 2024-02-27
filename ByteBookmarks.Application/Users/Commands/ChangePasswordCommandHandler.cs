#region

using ByteBookmarks.Application.Authentication;
using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class ChangePasswordCommandHandler(IUserRepository userRepository, IAuthService authService)
    : IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly IAuthService _authService = authService; // For password handling

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // 1. Retrieve the user
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException("User not found");

        // 2. Verify the current password
        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
            throw new Exception("Incorrect current password");

        // 3. Hash the new password
        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        // 4. Update the user's password
        user.Password = newPasswordHash;

        // 5. Persist the change
        await userRepository.UpdateUserAsync(user);

        return Unit.Value;
    }
}