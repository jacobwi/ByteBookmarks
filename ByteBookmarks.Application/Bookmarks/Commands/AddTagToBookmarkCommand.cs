#region

using System.Text.Json.Serialization;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddTagToBookmarkCommand : IRequest<string>
{
    public int BookmarkId { get; set; }
    public string TagName { get; set; }

    [JsonIgnore] public string UserId { get; set; }
}