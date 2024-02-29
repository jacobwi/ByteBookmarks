#region

using ByteBookmarks.Application.Bookmarks.Commands;
using Nelibur.ObjectMapper;
// Assuming domain entities are in this namespace
// Assuming DTOs are in this namespace

#endregion

namespace ByteBookmarks.Application;

public static class MappingConfiguration
{
    public static void Configure()
    {
        // Bind command DTOs to commands
        TinyMapper.Bind<LoginDto, LoginUserCommand>();
        TinyMapper.Bind<SignupDto, RegisterUserCommand>();
        TinyMapper.Bind<BookmarkDto, CreateBookmarkCommand>();
        TinyMapper.Bind<BookmarkDto, UpdateBookmarkCommand>();

        // Entity to DTO mappings for simple properties
        TinyMapper.Bind<Category, BookmarkCategoryDto>();
        TinyMapper.Bind<Tag, BookmarkTagDto>();
        TinyMapper.Bind<Image, BookmarkImageDto>();

        // Mapping for Bookmark to BookmarkDto
        // Ignoring complex properties to handle them manually later
        TinyMapper.Bind<Bookmark, BookmarkDto>(config => { });
    }
}