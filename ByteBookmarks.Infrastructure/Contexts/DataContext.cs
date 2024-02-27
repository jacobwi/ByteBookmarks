#region

using ByteBookmarks.Core.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ByteBookmarks.Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<Image> Images { get; set; }
}