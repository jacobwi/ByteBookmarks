#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Commands;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            UserId = request.UserId
        };

        await categoryRepository.CreateCategoryAsync(category);

        return new CategoryDto
        {
            Id = category.CategoryId,
            Name = category.Name
        };
    }
}