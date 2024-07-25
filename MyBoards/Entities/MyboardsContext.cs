using Microsoft.EntityFrameworkCore;
using MyBoards.Entities.ViewModels;
namespace MyBoards.Entities
{
    public class MyboardsContext : DbContext
    {
        public MyboardsContext(DbContextOptions<MyboardsContext> options) : base(options) 
        {
        }
        public DbSet<TopAuthor> ViewTopAuthors { get; set; }
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
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("Getutdate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
                eb.HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasOne(x => x.Address).WithOne(a => a.User).HasForeignKey<Address>(a => a.UserId);
            });
            modelBuilder.Entity<WorkItemState>()
                .HasData(new WorkItemState() { Value = "To Do" , Id = 1 },
                new WorkItemState() { Value = "Doing" , Id = 2},
                new WorkItemState() { Value = "Done" , Id = 3});
            modelBuilder.Entity<Tag>()
                .HasData(new Tag() { Value = "Web", Id = 1 },
                new Tag() { Value = "UI", Id = 2 },
                new Tag() { Value = "Desktop", Id = 3 },
                new Tag() { Value = "API", Id = 4 },
                new Tag() { Value = "Service", Id = 5 });

            modelBuilder.Entity<TopAuthor>(eb =>
            {
                eb.ToView("View_TopAuthors");
                eb.HasNoKey();
            });

        }
    }
}
