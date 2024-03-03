#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace ByteBookmarks.Core.Entities;

[PrimaryKey(nameof(TagId), nameof(BookmarkId))]
public class TagBookmark
{
    public int TagId { get; set; }

    public virtual Tag Tag { get; set; }

    public int BookmarkId { get; set; }

    public virtual Bookmark Bookmark { get; set; }
}