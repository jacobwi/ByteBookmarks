#region

using ByteBookmarks.Application.Users.DTOs;
using ByteBookmarks.Core.Exceptions;

#endregion

namespace ByteBookmarks.Application.Users.Queries;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserByIdQuery, ClientUser>
{
    public async Task<ClientUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user == null) throw new EntityNotFoundException(nameof(ApplicationUser), request.UserId);

        return new ClientUser
        {
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }
}