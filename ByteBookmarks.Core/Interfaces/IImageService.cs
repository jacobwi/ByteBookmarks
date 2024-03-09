#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IImageService
{
    Task<Image?> UploadImageAsync(RelationshipType relationshipType, string relationshipId, Stream imageData,
        string fileName, string contentType);

    Task DeleteImageAsync(string imageId);
    Task<Image?> GetImageAsync(string imageId);
}