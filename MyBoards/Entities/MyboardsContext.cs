using Microsoft.EntityFrameworkCore;
namespace MyBoards.Entities
{
    public class MyboardsContext : DbContext
    {
        public MyboardsContext(DbContextOptions<MyboardsContext> options) : base(options) 
        {
        }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<WorkItemState> States { get; set; }
        public DbSet<WorkItemTag> WorkItemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.HasOne(x => x.State).WithMany().HasForeignKey(c => c.StateId);
            }
                ) ;
            modelBuilder.Entity<WorkItemState>(eb =>
            {
                eb.Property(x => x.Value).IsRequired();
                eb.Property(y => y.Value).HasMaxLength(50);
            }
    );
            modelBuilder.Entity<Epic>()
                .Property(xi => xi.StartDate)
                .HasPrecision(3);
            modelBuilder.Entity<Epic>()
                .Property(xi => xi.EndDate)
                .HasPrecision(3);
            modelBuilder.Entity<Task>().
                Property(xx => xx.Activity).
                HasMaxLength(200);
            modelBuilder.Entity<Task>()
                .Property(xs => xs.RemaningWork).
                HasPrecision(14, 2);
            modelBuilder.Entity<Issue>()
                .Property(ss => ss.Efford)
                .HasColumnType("decimal(5,2)");
            modelBuilder.Entity<WorkItem>().
                Property(x => x.area).
                HasColumnType("Varchar(200)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(xx => xx.Priority).HasDefaultValue(1);
                eb.HasMany(xa => xa.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);
                eb.HasOne(o => o.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(u => u.AuthorId);
                eb.HasMany(w => w.Tags)
                .WithMany(u => u.WorkItems)
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag)
                    .WithMany()
                    .HasForeignKey(u => u.TagId),

                    w => w.HasOne(wit => wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(u => u.WorkItemId),
                    wit =>
                    {
                        wit.HasKey(X => new { X.WorkItemId, X.TagId });
                        wit.Property(X => X.PublicationDate).HasDefaultValueSql("Getutdate()");
                    }
                    );
            });
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutDate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
            });
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasOne(x => x.Address).WithOne(a => a.User).HasForeignKey<Address>(a => a.UserId);
            });
        }
    }
}
