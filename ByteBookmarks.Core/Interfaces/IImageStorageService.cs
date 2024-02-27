#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IImageStorageService
{
    Task SaveImageAsync(Image image, Stream imageData);
    Task<Stream> GetImageAsync(string imageId);
    Task DeleteImageAsync(string imageId);
}