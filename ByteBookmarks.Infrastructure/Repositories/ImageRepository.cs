#region

using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using ByteBookmarks.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ByteBookmarks.Infrastructure.Repositories;

public class ImageRepository(DataContext context) : IImageRepository
{
    public async Task<Image> GetImageByIdAsync(int imageId)
    {
        return await context.Images
            .Where(i => i.Id == imageId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Image image)
    {
        await context.Images.AddAsync(image);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Image image)
    {
        context.Images.Remove(image);
        await context.SaveChangesAsync();
    }
}