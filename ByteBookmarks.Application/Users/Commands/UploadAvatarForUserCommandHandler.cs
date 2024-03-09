#region

using ByteBookmarks.Core.Exceptions;

#endregion

namespace ByteBookmarks.Application.Users.Commands;

public class UploadAvatarForUserCommandHandler(IImageService imageService, IUserService userService)
    : IRequestHandler<UploadAvatarForUserCommand, bool>
{
    public async Task<bool> Handle(UploadAvatarForUserCommand request, CancellationToken cancellationToken)
    {
        var file = request.File;
        var userId = request.UserId;

        // Update user profile with new image
        var userProfile = await userService.GetUserProfileAsync(userId);

        if (userProfile == null) throw new EntityNotFoundException(nameof(UserProfile), userId);


        // Upload image to storage
        var image = await imageService.UploadImageAsync(RelationshipType.ProfileAvatar, userId,
            file.OpenReadStream(), file.FileName, file.ContentType);

        if (image == null) throw new Exception("Failed to upload image");

        userProfile.AvatarId = image.Id;
        await userService.UpdateUserProfileAsync(userProfile);

        return true;
    }
}