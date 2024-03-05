#region

using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksWithPaginationQueryHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<GetBookmarksWithPaginationQuery, List<BookmarkDto>>
{
    public async Task<List<BookmarkDto>> Handle(GetBookmarksWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var bookmarks =
            await bookmarkRepository.GetBookmarksByUserIdAsync(request.UserId, request.Page, request.PageSize);
        var bookmarkDtos = TinyMapper.Map<List<BookmarkDto>>(bookmarks);
        return bookmarkDtos;
    }
}