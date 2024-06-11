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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>().
                Property(x => x.state).
                IsRequired();
            modelBuilder.Entity<WorkItem>().
                Property(x => x.area).
                HasColumnType("Varchar(200)");
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(xi => xi.StartDate).HasPrecision(3);
                eb.Property(yi => yi.EndDate).HasPrecision(3);
                eb.Property(ss => ss.Efford).HasColumnType("decimal(5,2)");
                eb.Property(xx => xx.Activity).HasMaxLength(200);
                eb.Property(xs => xs.RemaningWork).HasPrecision(14, 2);
                eb.Property(xx => xx.Priority).HasDefaultValue(1);
            });
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutDate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
            });
        }
    }
}
