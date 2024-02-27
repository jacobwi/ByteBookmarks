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
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [ForeignKey("Bookmark")] // Name of the navigation property
    public int BookmarkId { get; set; }

    public Bookmark Bookmark { get; set; }
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