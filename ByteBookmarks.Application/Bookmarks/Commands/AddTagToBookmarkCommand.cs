#region

#endregion

#region

using ByteBookmarks.Application.Tags;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddTagToBookmarkCommand : IRequest<TagDto>
{
    public int BookmarkId { get; set; }
    public string TagName { get; set; }
}