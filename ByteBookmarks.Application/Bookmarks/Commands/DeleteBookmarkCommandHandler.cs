#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class DeleteBookmarkCommandHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<DeleteBookmarkCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
    {
        var bookmark = await bookmarkRepository.GetBookmarkByIdAsync(request.Id);

        if (bookmark == null) throw new KeyNotFoundException("Bookmark not found");

        // Authorization Check (if needed)
        if (bookmark.UserId != request.UserId)
            throw new UnauthorizedAccessException("Cannot delete another user's bookmark");

        await bookmarkRepository.DeleteBookmarkAsync(bookmark);

        return Unit.Value; // Indicates successful deletion
    }
}