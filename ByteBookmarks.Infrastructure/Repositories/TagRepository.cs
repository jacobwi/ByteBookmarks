namespace ByteBookmarks.Infrastructure.Repositories;

public class TagRepository(DataContext context) : ITagRepository
{
    public async Task<IEnumerable<Tag>> GetUserTags(string userId)
    {
        return await context.Tags.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Tag>> GetUserTags(string userId, int page, int pageSize)
    {
        return await context.Tags
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.TagId) // Assuming you want to order by TagId, adjust as necessary
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Tag> GetTag(int id, string? userId)
    {
        return await context.Tags.FirstOrDefaultAsync(t => t.TagId == id && t.UserId == userId);
    }

    public async Task<Tag> CreateTag(Tag tag)
    {
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
        return tag;
    }

    public async Task<bool> DeleteTag(int id, string? userId)
    {
        var tag = await context.Tags.FirstOrDefaultAsync(t => t.TagId == id && t.UserId == userId);
        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTag(Tag tag)
    {
        context.Tags.Update(tag);
        await context.SaveChangesAsync();
        return true;
    }
}