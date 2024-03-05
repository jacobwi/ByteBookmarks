#region

using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Queries;

public class GetBookmarksByUserIdQueryHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<GetBookmarksByUserIdQuery, List<BookmarkDto>>
{
    public async Task<List<BookmarkDto>> Handle(GetBookmarksByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var bookmarks = await bookmarkRepository.GetBookmarksByUserIdAsync(request.UserId);
        var bookmarkDtos = TinyMapper.Map<List<BookmarkDto>>(bookmarks);
        return bookmarkDtos;
    }
}