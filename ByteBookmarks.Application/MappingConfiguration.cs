using ByteBookmarks.Application.Authentication;
using ByteBookmarks.Application.Users.Commands;
using Nelibur.ObjectMapper;

namespace ByteBookmarks.Application;

public static class MappingConfiguration 
{
    public static void Configure()
    {
        TinyMapper.Bind<LoginDto, LoginUserCommand>();
        TinyMapper.Bind<SignupDto, RegisterUserCommand>();
    }
}