#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Queries;

public class GetCategoriesByUserIdQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesByUserIdQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetCategoriesByUserIdAsync(request.UserId);
        return categories.Select(c => new CategoryDto
        {
            Id = c.CategoryId,
            Name = c.Name
        });
    }
}