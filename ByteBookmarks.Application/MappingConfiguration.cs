#region

using ByteBookmarks.Application.Bookmarks.Commands;
using ByteBookmarks.Application.Categories.Commands;
using ByteBookmarks.Application.Categories.DTOs;
using ByteBookmarks.Application.Tags;
using ByteBookmarks.Application.Tags.Commands;
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
        TinyMapper.Bind<CategoryDto, CreateCategoryCommand>();
        TinyMapper.Bind<CategoryDto, UpdateCategoryCommand>();
        TinyMapper.Bind<TagDto, CreateTagCommand>();
        TinyMapper.Bind<TagDto, UpdateTagCommand>();

        // Entity to DTO mappings for simple properties
        TinyMapper.Bind<UserProfile, UserProfileDto>();
        TinyMapper.Bind<Category, BookmarkCategoryDto>(config =>
        {
            config.Bind(source => source.CategoryId, target => target.Id);
        });
        TinyMapper.Bind<Tag, BookmarkTagDto>(config =>
        {
            // Assuming Tag has an Id property and BookmarkTagDto has a matching Id property
            config.Bind(source => source.TagId, target => target.Id);

            // Map other properties as needed
        });
        TinyMapper.Bind<Image, BookmarkImageDto>();

        TinyMapper.Bind<Category, CategoryDto>();
        TinyMapper.Bind<TagBookmark, BookmarkTagDto>(config =>
        {
            // Assuming TagBookmark has a navigation property to Tag
            config.Bind(source => source.Tag.TagId, target => target.Id);
            config.Bind(source => source.Tag.Name, target => target.Name);
            // Map other properties as necessary
        });
        TinyMapper.Bind<Bookmark, BookmarkDto>(config =>
        {
            config.Bind(source => source.CategoryBookmarks, target => target.Categories);
            config.Bind(source => source.TagBookmarks, target => target.Tags);
            // Map other properties as necessary
        });

        TinyMapper.Bind<CategoryBookmark, BookmarkCategoryDto>(config => { });
    }
}