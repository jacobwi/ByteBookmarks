#region

using ByteBookmarks.Application.Bookmarks.DTOs;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarkByIdQuery : IRequest<BookmarkDto>
{
    public GetBookmarkByIdQuery(int id, string userId)
    {
        Id = id;
        UserId = userId;
    }

    public int Id { get; set; }
    public string UserId { get; set; }
}