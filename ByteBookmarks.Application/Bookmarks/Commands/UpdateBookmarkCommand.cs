#region

using MediatR;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class UpdateBookmarkCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string URL { get; set; }

    // ... other bookmark properties ... 
    public string? UserId { get; set; } // For authorization
}