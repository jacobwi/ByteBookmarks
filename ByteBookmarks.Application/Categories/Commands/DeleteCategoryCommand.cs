namespace ByteBookmarks.Application.Categories.Commands;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}