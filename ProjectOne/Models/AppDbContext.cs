using Microsoft.EntityFrameworkCore;
using ProjectFuse.Models.Forum;

namespace ProjectFuse.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ForumMessage> ForumMessages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
