#region

using ByteBookmarks.Application.Bookmarks.Commands;
using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Application;

public static class MappingConfiguration
{
    public static void Configure()
    {
        TinyMapper.Bind<LoginDto, LoginUserCommand>();
        TinyMapper.Bind<SignupDto, RegisterUserCommand>();
        TinyMapper.Bind<BookmarkDto, CreateBookmarkCommand>();
        TinyMapper.Bind<BookmarkDto, UpdateBookmarkCommand>();
    }
}