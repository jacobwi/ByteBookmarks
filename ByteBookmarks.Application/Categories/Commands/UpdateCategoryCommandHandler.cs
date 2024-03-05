namespace ByteBookmarks.Application.Categories.Commands;

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateCategoryCommand, bool>
{
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category == null) return false;

        category.Name = request.Name;
        await categoryRepository.UpdateCategoryAsync(category);

        return true;
    }
}