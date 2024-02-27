#region

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksQueryHandler : IRequestHandler<GetBookmarksQuery, IEnumerable<BookmarkDto>>
{
    private readonly IBookmarkRepository _bookmarkRepository;

    public GetBookmarksQueryHandler(IBookmarkRepository bookmarkRepository)
    {
        _bookmarkRepository = bookmarkRepository;
    }

    public async Task<IEnumerable<BookmarkDto>> Handle(GetBookmarksQuery request, CancellationToken cancellationToken)
    {
        // 1. Fetch bookmarks 
        var bookmarks =
            await _bookmarkRepository.GetBookmarksByUserIdAsync(request.UserId); // Update with your repository call

        // 2. Map the bookmarks to BookmarkDto objects

        // 3. Get Image


        var tasks = bookmarks.Select(async bookmark =>
        {
            var image = await GetBase64ImageAsync(bookmark.Image.Path);
            return new BookmarkDto
            {
                Id = bookmark.Id,
                Title = bookmark.Title,
                URL = bookmark.URL,
                Description = bookmark.Description,
                IsPasswordProtected = bookmark.IsPasswordProtected,
                Image = image
                // ... map other properties as needed
            };
        });

        var item = await Task.WhenAll(tasks);
        return item;
    }

    private async Task<string> GetBase64ImageAsync(string imagePath)
    {
        // 1. Get the image from the file system or cloud storage
        if (string.IsNullOrEmpty(imagePath)) return null;

        // 2. Get image bytes and convert to 64 string
        if (!File.Exists(imagePath)) return null;
        // 3. ReadAllBytesAsync
        var imageBytes = await File.ReadAllBytesAsync(imagePath);

        // 4. Convert the image to base64 string
        // 5. Return the base64 string
        return Convert.ToBase64String(imageBytes);
    }
}