#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class UpdateBookmarkCommandHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<UpdateBookmarkCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBookmarkCommand request, CancellationToken cancellationToken)
    {
        var bookmark = await bookmarkRepository.GetBookmarkByIdAsync(request.Id);

        if (bookmark == null) throw new KeyNotFoundException("Bookmark not found");

        // Authorization Check 
        if (bookmark.UserId != request.UserId)
            throw new UnauthorizedAccessException("Cannot update another user's bookmark");

        // Map properties from request to bookmark 
        bookmark.Title = request.Title;
        bookmark.URL = request.URL;
        // ... map other properties ...

        // Update using repository (implementation dependent)
        await bookmarkRepository.UpdateBookmarkAsync(bookmark, cancellationToken);

        return Unit.Value; // Indicate successful update
    }
}