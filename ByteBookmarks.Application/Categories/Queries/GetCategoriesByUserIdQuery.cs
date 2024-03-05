#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Queries;

public class GetCategoriesByUserIdQuery(string userId) : IRequest<IEnumerable<CategoryDto>>
{
    public string UserId { get; } = userId;
}