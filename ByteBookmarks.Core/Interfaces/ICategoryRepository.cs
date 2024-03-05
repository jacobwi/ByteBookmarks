#region

using ByteBookmarks.Core.Entities;

#endregion

namespace ByteBookmarks.Core.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category?>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category?> GetCategoryByNameAsync(string name);
    Task<Category?> CreateCategoryAsync(Category? category);
    Task DeleteCategoryAsync(Category? category);
    Task UpdateCategoryAsync(Category? category);
    Task<IEnumerable<Category?>> GetCategoriesByUserIdAsync(string userId);
    Task<IEnumerable<Category?>> GetCategoriesByUsernameAsync(string username);
    Task<IEnumerable<Category?>> GetCategoriesByUserIdAsync(string requestUserId, int requestPage, int requestPageSize);
}