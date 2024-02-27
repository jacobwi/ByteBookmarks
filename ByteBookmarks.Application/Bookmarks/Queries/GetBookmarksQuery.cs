#region

using ByteBookmarks.Application.Bookmarks.DTOs;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksQuery : IRequest<IEnumerable<BookmarkDto>>
{
    public GetBookmarksQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; } // Or get directly from authentication context
}