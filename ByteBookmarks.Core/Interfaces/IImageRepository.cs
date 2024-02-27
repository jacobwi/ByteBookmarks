#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface IImageRepository
{
    Task<Image> GetImageByIdAsync(int imageId);
    Task AddAsync(Image image);
    Task DeleteAsync(Image image);
}