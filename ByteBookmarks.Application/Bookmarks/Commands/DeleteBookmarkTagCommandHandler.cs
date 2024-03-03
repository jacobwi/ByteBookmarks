#region

using ByteBookmarks.Core.Exceptions;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class DeleteBookmarkTagCommandHandler(IBookmarkRepository bookmarkRepository, ITagRepository tagRepository)
    : IRequestHandler<DeleteBookmarkTagCommand, bool>
{
    public async Task<bool> Handle(DeleteBookmarkTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await bookmarkRepository.GetBookmarkByIdAsync(request.BookmarkId);

        if (entity == null) throw new EntityNotFoundException(nameof(entity), request.BookmarkId);


        return await tagRepository.DeleteTag(request.TagId, request.UserId);
    }
}