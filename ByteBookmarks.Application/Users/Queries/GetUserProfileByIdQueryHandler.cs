#region

using ByteBookmarks.Application.Users.DTOs;
using ByteBookmarks.Core.Exceptions;
using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Application.Users.Queries;

public class GetUserProfileByIdQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserProfileByIdQuery, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await userRepository.GetUserProfileAsync(request.UserId);
        if (userProfile == null) throw new EntityNotFoundException(nameof(UserProfile), request.UserId);
        // Map UserProfile to UserProfileDto
        var userProfileDto = TinyMapper.Map<UserProfileDto>(userProfile);

        // Convert image to base64
        if (userProfile.Avatar != null)
            userProfileDto.Avatar = await userProfile.Avatar.GetBase64ImageAsync(userProfile.Avatar.Path);

        return userProfileDto;
    }
}