namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddCategoryToBookmarkCommandHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<AddCategoryToBookmarkCommand, string>
{
    public async Task<string> Handle(AddCategoryToBookmarkCommand request, CancellationToken cancellationToken)
    {
        // 1. Fetch bookmark
        var bookmark =
            await bookmarkRepository.GetBookmarkByIdAsync(request.BookmarkId); // Update with your repository call

        if (bookmark == null) throw new KeyNotFoundException(bookmark.Id.ToString());
        // 2. Add category to bookmark
        bookmark.Categories.Add(new Category { Name = request.CategoryName });

        // 3. Update bookmark
        await bookmarkRepository.UpdateBookmarkAsync(bookmark, cancellationToken); // Update with your repository call

        return "Category added successfully";
    }
}