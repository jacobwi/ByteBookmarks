#region

#endregion

#region

using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksQueryHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<GetBookmarksQuery, IEnumerable<BookmarkDto>>
{
    public async Task<IEnumerable<BookmarkDto>> Handle(GetBookmarksQuery request, CancellationToken cancellationToken)
    {
        // 1. Fetch bookmarks 
        var bookmarks =
            await bookmarkRepository.GetBookmarksByUserIdAsync(request.UserId); // Update with your repository call

        // Map each Bookmark to BookmarkDto
        var bookmarkDtoTasks = bookmarks.Select(async bookmark =>
        {
            // Use TinyMapper for the direct properties
            var dto = TinyMapper.Map<BookmarkDto>(bookmark);

            // Manually map the Image property if it exists
            if (bookmark.Image != null) dto.Image.Base64Data = await GetBase64ImageAsync(bookmark.Image.Path);


            // // Manually map the Tags collection
            // dto.Tags = bookmark.Tags.Select(tag => TinyMapper.Map<BookmarkTagDto>(tag)).ToList();
            //
            // // Manually map the Categories collection
            // dto.Categories = bookmark.Categories.Select(category => TinyMapper.Map<BookmarkCategoryDto>(category))
            //     .ToList();

            return dto;
        }).ToList();
        var bookmarkDtos = await Task.WhenAll(bookmarkDtoTasks);

        return bookmarkDtos;
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