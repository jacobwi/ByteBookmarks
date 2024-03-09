#region

using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksQueryHandler : IRequestHandler<GetBookmarksQuery, IEnumerable<BookmarkDto>>
{
    private readonly IBookmarkRepository _bookmarkRepository;

    public GetBookmarksQueryHandler(IBookmarkRepository bookmarkRepository)
    {
        _bookmarkRepository = bookmarkRepository ?? throw new ArgumentNullException(nameof(bookmarkRepository));
    }

    public async Task<IEnumerable<BookmarkDto>> Handle(GetBookmarksQuery request, CancellationToken cancellationToken)
    {
        var bookmarks = await _bookmarkRepository.GetBookmarksByUserIdAsync(request.UserId);

        var bookmarkDtos = await Task.WhenAll(bookmarks.Select(MapBookmarkToDto));

        return bookmarkDtos;
    }

    private async Task<BookmarkDto> MapBookmarkToDto(Bookmark bookmark)
    {
        var dto = TinyMapper.Map<BookmarkDto>(bookmark);

        if (bookmark.Image != null)
            dto.Image.Base64Data = await bookmark.Image.GetBase64ImageAsync(bookmark.Image.Path);

        // Map TagBookmarks to BookmarkTagDto
        if (bookmark.TagBookmarks != null && bookmark.TagBookmarks.Count > 0)
            dto.Tags = bookmark.TagBookmarks
                .Select(tb => TinyMapper.Map<BookmarkTagDto>(tb.Tag))
                .ToList();

        // Map CategoryBookmarks to BookmarkCategoryDto
        if (bookmark.CategoryBookmarks != null && bookmark.CategoryBookmarks.Count > 0)
            dto.Categories = bookmark.CategoryBookmarks
                .Select(cb => TinyMapper.Map<BookmarkCategoryDto>(cb.Category))
                .ToList();

        return dto;
    }
}