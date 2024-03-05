#region

#endregion

namespace ByteBookmarks.Application.Tags.Queries;

public class GetTagsWithPaginationQueryHandler(ITagRepository tagRepository)
    : IRequestHandler<GetTagsWithPaginationQuery, IEnumerable<Tag>>
{
    public async Task<IEnumerable<Tag>> Handle(GetTagsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetUserTags(request.UserId, request.Page, request.PageSize);
        return tags;
    }
}