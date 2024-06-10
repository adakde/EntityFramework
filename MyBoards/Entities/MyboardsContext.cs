using Microsoft.EntityFrameworkCore;
namespace MyBoards.Entities
{
    public class MyboardsContext : DbContext
    {
        public MyboardsContext(DbContextOptions<MyboardsContext> options) : base(options) 
        {
        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> comments { get; set; }


    }
}
