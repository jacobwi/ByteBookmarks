#region

#endregion

namespace ByteBookmarks.Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryBookmark> CategoryBookmarks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagBookmark> TagBookmarks { get; set; }

    public DbSet<Image> Images { get; set; }
}