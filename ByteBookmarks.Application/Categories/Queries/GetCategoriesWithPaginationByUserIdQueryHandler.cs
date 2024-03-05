#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Queries;

public class
    GetCategoriesWithPaginationByUserIdQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesWithPaginationByUserIdQuery,
        IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesWithPaginationByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var categories =
            await categoryRepository.GetCategoriesByUserIdAsync(request.UserId, request.Page, request.PageSize);
        return categories.Select(c => new CategoryDto
        {
            Id = c.CategoryId,
            Name = c.Name
        });
    }
}