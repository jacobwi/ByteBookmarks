#region

using ByteBookmarks.Application.Bookmarks.DTOs;
using ByteBookmarks.Core.Entities;
using ByteBookmarks.Core.Interfaces;
using MediatR;

#endregion

namespace ByteBookmarks.Application.Bookmarks.Commands;

public class CreateBookmarkCommandHandler(
    IBookmarkRepository bookmarkRepository,
    IImageStorageService imageStorageService)
    : IRequestHandler<CreateBookmarkCommand, NewBookmarkDto>
{
    public async Task<NewBookmarkDto> Handle(CreateBookmarkCommand request, CancellationToken cancellationToken)
    {
        Image image = null;
        // Hash password if provided
        if (request.IsPasswordProtected) request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        // Process the image file
        if (request.Image == null)
        {
        }
        else
        {
            image = new Image
            {
                Name = request.Image.FileName,
                Extension = Path.GetExtension(request.Image.FileName),
                ContentType = request.Image.ContentType,
                Size = request.Image.Length,
                UserId = request.UserId,
                RelationshipType = RelationshipType.BookmarkThumbnail,
                StoreType = StorageType.Local
            };
            await imageStorageService.SaveImageAsync(image, request.Image.OpenReadStream());


            // TODO: Move the below to localstorage service
            // var filePath = Path.Combine("Uploads", request.Image.FileName);
            // await using var stream = new FileStream(filePath, FileMode.Create);
            // await request.Image.CopyToAsync(stream, cancellationToken);
            // // Save the file path or any other relevant information in your database
        }

        var bookmark = new Bookmark
        {
            Title = request.Title,
            URL = request.URL,
            Description = request.Description,
            IsPasswordProtected = request.IsPasswordProtected,
            Password = request.Password,
            UserId = request.UserId,
            Image = image,
            ImageId = image?.Id ?? 0
        };

        await bookmarkRepository.AddBookmarkAsync(bookmark);

        // Map bookmark to BookmarkDto for response
        return new NewBookmarkDto
        {
            Id = bookmark.Id,
            Title = bookmark.Title,
            URL = bookmark.URL,
            Description = bookmark.Description,
            IsPasswordProtected = bookmark.IsPasswordProtected

            // ... other properties
        };
    }
}