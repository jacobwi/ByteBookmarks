#region

using ByteBookmarks.Core.Exceptions;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddCategoryToBookmarkCommandHandler(
    IBookmarkRepository bookmarkRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<AddCategoryToBookmarkCommand, string>
{
    public async Task<string> Handle(AddCategoryToBookmarkCommand request, CancellationToken cancellationToken)
    {
        var bookmark = await bookmarkRepository.GetBookmarkByIdAsync(request.BookmarkId);
        if (bookmark == null) throw new EntityNotFoundException(nameof(Bookmark), request.BookmarkId);

        var category = await categoryRepository.GetCategoryByIdAsync(request.CategoryId);
        if (category == null) throw new EntityNotFoundException(nameof(Category), request.CategoryId);

        var categoryBookmark = new CategoryBookmark
        {
            BookmarkId = bookmark.Id,
            CategoryId = category.CategoryId
        };

        bookmark.CategoryBookmarks.Add(categoryBookmark);

        await bookmarkRepository.UpdateBookmarkAsync(bookmark, cancellationToken);

        return "Category added to bookmark successfully";
    }
}