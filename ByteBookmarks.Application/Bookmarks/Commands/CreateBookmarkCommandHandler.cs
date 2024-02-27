#region

using ByteBookmarks.Application.Bookmarks.DTOs;
using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class CreateBookmarkCommandHandler(IBookmarkRepository bookmarkRepository)
    : IRequestHandler<CreateBookmarkCommand, BookmarkDto>
{
    public async Task<BookmarkDto> Handle(CreateBookmarkCommand request, CancellationToken cancellationToken)
    {
        // Hash password if provided
        if (request.IsPasswordProtected) request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var bookmark = new Bookmark
        {
            Title = request.Title,
            URL = request.URL,
            Description = request.Description,
            IsPasswordProtected = request.IsPasswordProtected,
            Password = request.Password,
            UserId = request.UserId
        };

        await bookmarkRepository.AddBookmarkAsync(bookmark);

        // Map bookmark to BookmarkDto for response
        return new BookmarkDto
        {
            Id = bookmark.Id,
            Title = bookmark.Title
            // ... other properties
        };
    }
}