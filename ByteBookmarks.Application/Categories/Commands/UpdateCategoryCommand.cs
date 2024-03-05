namespace ByteBookmarks.Application.Categories.Commands;

public class UpdateCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
}