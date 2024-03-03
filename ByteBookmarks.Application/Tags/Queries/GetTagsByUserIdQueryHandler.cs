#region

using ByteBookmarks.Core.Exceptions;

#endregion

namespace ByteBookmarks.Application.Tags.Queries;

public class GetTagsByUserIdQueryHandler(ITagRepository tagRepository, IUserRepository userRepository)
    : IRequestHandler<GetTagsByUserIdQuery, IEnumerable<TagDto>>
{
    public async Task<IEnumerable<TagDto>> Handle(GetTagsByUserIdQuery request, CancellationToken cancellationToken)
    {
        // Check if user exists in the database
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user == null) throw new EntityNotFoundException(nameof(ApplicationUser), request.UserId);

        // Get the user's tags
        var tags = await tagRepository.GetUserTags(request.UserId);
        return tags.Select(t => new TagDto
        {
            Id = t.TagId,
            Name = t.Name,
            UserId = t.UserId
        }).ToList();
    }
}