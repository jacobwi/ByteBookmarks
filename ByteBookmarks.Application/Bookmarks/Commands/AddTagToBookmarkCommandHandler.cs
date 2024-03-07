#region

using ByteBookmarks.Application.Tags;
using ByteBookmarks.Core.Exceptions;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class AddTagToBookmarkCommandHandler(
    IBookmarkRepository bookmarkRepository,
    ITagRepository tagRepository,
    IUserService userService)
    : IRequestHandler<AddTagToBookmarkCommand, TagDto>
{
    public async Task<TagDto> Handle(AddTagToBookmarkCommand request, CancellationToken cancellationToken)
    {
        // GET THE CURRENT USER
        var user = await userService.GetCurrentUserAsync();
        if (user == null) throw new EntityNotFoundException(nameof(user));

        // GET THE BOOKMARK
        var bookmark = await bookmarkRepository.GetBookmarkByIdAsync(request.BookmarkId);
        if (bookmark == null) throw new EntityNotFoundException(nameof(bookmark), $"{request.BookmarkId}");

        // GET THE TAG
        var tag = (await tagRepository.GetUserTags(user.Id)).FirstOrDefault(t => t.Name == request.TagName);

        // IF THE TAG DOESN'T EXIST, CREATE IT
        if (tag == null)
        {
            tag = new Tag
            {
                Name = request.TagName,
                UserId = user.Id
            };
            await tagRepository.CreateTag(tag);
        }

        // ADD THE TAG TO THE BOOKMARK async
        await bookmarkRepository.AddTagToBookmarkAsync(bookmark, tag, cancellationToken);


        // UPDATE THE BOOKMARK
        await bookmarkRepository.UpdateBookmarkAsync(bookmark, cancellationToken);


        // Return the tag
        return new TagDto
        {
            Id = tag.TagId,
            Name = tag.Name
        };
    }
}