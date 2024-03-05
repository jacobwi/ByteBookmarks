#region

using ByteBookmarks.Application.Categories.DTOs;

#endregion

namespace ByteBookmarks.Application.Categories.Commands;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; set; }
    public string UserId { get; set; }
}