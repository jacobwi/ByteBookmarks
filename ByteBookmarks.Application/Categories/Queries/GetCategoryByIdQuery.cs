namespace ByteBookmarks.Application.Categories.Queries;

public class GetCategoryByIdQuery(int id) : IRequest<object?>
{
    public int Id { get; } = id;
}