#region

using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace ByteBookmarks.Core.Entities;

public class Image
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public StorageType StoreType { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }

    // Nullable FK to ApplicationUser. Only set if RelationshipType is ProfileAvatar
    [ForeignKey("User")] public string? UserId { get; set; }

    public virtual ApplicationUser? User { get; set; }

    // Nullable FK to Bookmark. Only set if RelationshipType is BookmarkThumbnail
    [ForeignKey("Bookmark")] public int? BookmarkId { get; set; }

    public virtual Bookmark? Bookmark { get; set; }


    public async Task<string> GetBase64ImageAsync(string imagePath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath)) return null;

            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            return Convert.ToBase64String(imageBytes);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to convert image to base64: {e.Message}");
            return null;
        }
    }
}

public enum RelationshipType
{
    ProfileAvatar,
    BookmarkThumbnail
}

public enum StorageType
{
    Local,
    S3,
    Azure,
    Dropbox,
    GoogleDrive
}