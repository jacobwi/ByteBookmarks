#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Queries;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoryByIdQuery, object?>
{
    public async Task<object?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(request.Id);
        return category == null
            ? null
            : new CategoryDto
            {
                Id = category.CategoryId,
                Name = category.Name
            };
    }
}