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
        public DbSet<WorkIteamState> States { get; set; }
        public DbSet<WorkItemTag> WorkItemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.HasOne(x => x.State).WithMany().HasForeignKey(c => c.StateId);
            }
                ) ;
            modelBuilder.Entity<WorkIteamState>(eb =>
            {
                eb.Property(x => x.Value).IsRequired();
                eb.Property(y => y.Value).HasMaxLength(50);
            }
    );
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
                eb.HasMany(xa => xa.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItem.Id);
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
                        wit.HasKey(X => new { X.WorkIteamId, X.TagId });
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
