#region

using ByteBookmarks.Application.Bookmarks.DTOs;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class CreateBookmarkCommand : IRequest<BookmarkDto>
{
    public string Title { get; set; }
    public string URL { get; set; }
    public string Description { get; set; }
    public bool IsPasswordProtected { get; set; }
    public string Password { get; set; }
    public string? UserId { get; set; }
}