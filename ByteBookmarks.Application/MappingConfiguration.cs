#region

using ByteBookmarks.Application.Bookmarks.Commands;
using ByteBookmarks.Application.Categories.DTOs;
using ByteBookmarks.Application.Users.DTOs;
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


        TinyMapper.Bind<Category, CategoryDto>();


        // Map tagBookmark and categoryBookmark to their DTOs
        TinyMapper.Bind<TagBookmark, BookmarkTagDto>(config =>
        {
            config.Bind(source => source.Tag.Name, target => target.Name);
            config.Bind(source => source.Tag.TagId, target => target.Id);
        });
        TinyMapper.Bind<CategoryBookmark, BookmarkCategoryDto>(config =>
        {
            config.Bind(source => source.Category.Name, target => target.Name);
        });


        // Mapping for Bookmark to BookmarkDto
        // Ignoring complex properties to handle them manually later
        TinyMapper.Bind<Bookmark, BookmarkDto>(config => { });
    }
}