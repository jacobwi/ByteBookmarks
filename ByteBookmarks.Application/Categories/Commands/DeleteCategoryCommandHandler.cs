#region

#endregion

namespace ByteBookmarks.Application.Categories.Commands;

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category == null) return false;

        await categoryRepository.DeleteCategoryAsync(category);
        return true;
    }
}