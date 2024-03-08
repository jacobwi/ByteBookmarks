#region

#endregion

namespace ByteBookmarks.Infrastructure.Services;

public class LocalImageStorageService : IImageStorageService
{
    private readonly string _rootFolder;

    public LocalImageStorageService(IConfiguration configuration)
    {
        _rootFolder = configuration["storage:local:rootFolder"];
        Directory.CreateDirectory(_rootFolder); // Ensure the root folder exists
    }

    public async Task SaveImageAsync(Image? image, Stream imageData)
    {
        var filePath = GetFilePath(image);

        await using (var fileStream = File.Create(filePath))
        {
            await imageData.CopyToAsync(fileStream);
        }

        // Update the Image object with the storage location if needed
        image.Path = filePath; // Adjust if necessary in your system
    }

    public async Task<Stream> GetImageAsync(string imageId)
    {
        var filePath = GetFilePathById(imageId); // You'll need this logic

        if (!File.Exists(filePath)) throw new FileNotFoundException($"Image with ID '{imageId}' not found.");

        return File.OpenRead(filePath);
    }

    public async Task DeleteImageAsync(string imageId)
    {
        var filePath = GetFilePathById(imageId);

        if (File.Exists(filePath)) File.Delete(filePath);
    }

    // Helper to get the file path based on your image storage logic
    private string GetFilePath(Image? image)
    {
        // Example: Store based on relationship type and further subdirectories
        var folder = Path.Combine(_rootFolder, image.RelationshipType.ToString());
        Directory.CreateDirectory(folder); // Ensure the folder exists
        return Path.Combine(folder, Guid.NewGuid() + Path.GetExtension(image.Name));
    }

    // Helper to get the file path based on an existing image ID
    private string GetFilePathById(string imageId)
    {
        // TODO: Implement your logic to get the file path based on the image ID
        return "";
    }
}