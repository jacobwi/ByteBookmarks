namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddTagToBookmarkCommandHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<AddTagToBookmarkCommand, string>
{
    public async Task<string> Handle(AddTagToBookmarkCommand request, CancellationToken cancellationToken)
    {
        // 1. Fetch bookmark
        var bookmark =
            await bookmarkRepository.GetBookmarkByIdAsync(request.BookmarkId); // Update with your repository call

        if (bookmark == null) throw new KeyNotFoundException(bookmark.Id.ToString());
        // 2. Add tag to bookmark
        bookmark.Tags.Add(new Tag { Name = request.TagName, UserId = request.UserId });

        // 3. Update bookmark
        await bookmarkRepository.UpdateBookmarkAsync(bookmark, cancellationToken); // Update with your repository call

        return "Tag added successfully";
    }
}