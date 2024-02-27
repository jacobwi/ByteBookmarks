#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class DeleteBookmarkCommand(int id, string? userId) : IRequest<Unit>
{
    public int Id { get; set; } = id;
    public string UserId { get; set; } = userId;

    // Add a constructor to accept the arguments
}