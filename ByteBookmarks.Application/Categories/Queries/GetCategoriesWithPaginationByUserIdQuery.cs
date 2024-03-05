#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Queries;

public class GetCategoriesWithPaginationByUserIdQuery(string userId, int page, int pageSize)
    : IRequest<IEnumerable<CategoryDto>>
{
    public string UserId { get; } = userId;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}