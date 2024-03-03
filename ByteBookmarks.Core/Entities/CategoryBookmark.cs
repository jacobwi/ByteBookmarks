#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace ByteBookmarks.Core.Entities;

[PrimaryKey(nameof(CategoryId), nameof(BookmarkId))]
public class CategoryBookmark
{
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public int BookmarkId { get; set; }

    public virtual Bookmark Bookmark { get; set; }
}