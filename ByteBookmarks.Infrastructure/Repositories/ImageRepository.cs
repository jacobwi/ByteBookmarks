#region

#endregion

namespace ByteBookmarks.Infrastructure.Repositories;

public class ImageRepository(DataContext context) : IImageRepository
{
    public async Task<Image?> GetImageByIdAsync(int imageId)
    {
        return await context.Images
            .Where(i => i.Id == imageId)
            .FirstOrDefaultAsync();
    }

    public async Task<int> AddAsync(Image? image)
    {
        await context.Images.AddAsync(image);
        await context.SaveChangesAsync();
        return image.Id;
    }

    public async Task DeleteAsync(Image? image)
    {
        context.Images.Remove(image);
        await context.SaveChangesAsync();
    }
}