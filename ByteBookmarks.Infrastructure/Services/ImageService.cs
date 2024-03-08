#region

#endregion

namespace ByteBookmarks.Infrastructure.Services;

public class ImageService(
    IImageStorageService storageService,
    IImageRepository imageRepository,
    IConfiguration configuration)
{
    public async Task<Image?> UploadImageAsync(string relationshipType, string userId, Stream imageData,
        string fileName,
        string contentType)
    {
        // Basic validation
        if (!IsValidImageContentType(contentType)) throw new ArgumentException("Invalid image content type");

        var image = new Image
        {
            Name = fileName,
            RelationshipType = (RelationshipType)Enum.Parse(typeof(RelationshipType), relationshipType),
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


    private bool IsValidImageContentType(string contentType)
    {
        // TODO: Validate the content type
        return true;
    }
}