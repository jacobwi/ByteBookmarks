#region

#endregion

namespace ByteBookmarks.Infrastructure.Services;

public class ImageService(
    IImageStorageService storageService,
    IImageRepository imageRepository,
    IConfiguration configuration) : IImageService
{
    public async Task<Image?> UploadImageAsync(RelationshipType relationshipType, string userId, Stream imageData,
        string fileName,
        string contentType)
    {
        // Basic validation
        if (!IsValidImageContentType(contentType)) throw new ArgumentException("Invalid image content type");

        var image = new Image
        {
            Name = fileName,
            RelationshipType = relationshipType,
            UserId = userId,
            ContentType = contentType,
            StoreType = Enum.Parse<StorageType>(configuration["Storage:StorageType"]),
            Extension = Path.GetExtension(fileName),
            Size = imageData.Length
        };

        await storageService.SaveImageAsync(image, imageData);

        // Assuming you have an image repository to persist the metadata
        await imageRepository.AddAsync(image);

        return image;
    }

    public Task DeleteImageAsync(string imageId)
    {
        throw new NotImplementedException();
    }

    public Task<Image?> GetImageAsync(string imageId)
    {
        throw new NotImplementedException();
    }


    private bool IsValidImageContentType(string contentType)
    {
        // TODO: Validate the content type
        return true;
    }
}