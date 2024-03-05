namespace ByteBookmarks.Infrastructure.Repositories;

public class CategoryRepository(DataContext context) : ICategoryRepository
{
    public async Task<IEnumerable<Category?>> GetCategoriesAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await context.Categories.FirstOrDefaultAsync(x => x != null && x.Name == name);
    }

    public async Task<Category?> CreateCategoryAsync(Category? category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Category? category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category? category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category?>> GetCategoriesByUserIdAsync(string userId)
    {
        return await context.Categories.Where(x => x != null && x.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Category?>> GetCategoriesByUsernameAsync(string username)
    {
        return await context.Categories.Where(x => x != null && x.User.Username == username).ToListAsync();
    }

    public async Task<IEnumerable<Category?>> GetCategoriesByUserIdAsync(string requestUserId, int requestPage,
        int requestPageSize)
    {
        return await context.Categories
            .Where(x => x != null && x.UserId == requestUserId)
            .Skip(requestPage * requestPageSize)
            .Take(requestPageSize)
            .ToListAsync();
    }
}