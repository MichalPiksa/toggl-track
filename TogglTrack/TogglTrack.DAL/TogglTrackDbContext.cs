using Microsoft.EntityFrameworkCore;

namespace TogglTrack.DAL
{
    public class TogglTrackDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ActivityEntity> Activities { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public TogglTrackDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasMany(user => user.Projects)
                .WithMany(project => project.Users);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(project => project.Activities)
                .WithOne(activity => activity.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ActivityEntity>()
                .HasOne(activity => activity.User)
                .WithMany(user => user.Activities)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(result => System.Diagnostics.Trace.WriteLine(result), Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseSeeding((context, services) =>
            {
                var user1 = context.Set<UserEntity>().FirstOrDefault(u => u.FirstName == "Carlos" && u.LastName == "Wilkinson");
                var user2 = context.Set<UserEntity>().FirstOrDefault(u => u.FirstName == "Liam" && u.LastName == "Henderson");
                if (user1 == null)
                {
                    context.Set<UserEntity>().Add(new UserEntity
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Carlos",
                        LastName = "Wilkinson",
                        PhotoUrl = "https://cdn.vectorstock.com/i/500p/17/61/male-avatar-profile-picture-vector-10211761.jpg",
                    });
                    context.SaveChanges();
                }
                if (user2 != null)
                {
                    context.Set<UserEntity>().Add(new UserEntity
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Liam",
                        LastName = "Henderson",
                        PhotoUrl = "https://cdn.vectorstock.com/i/500p/17/61/male-avatar-profile-picture-vector-10211761.jpg"
                    });
                    context.SaveChanges();
                }
            });
        }
    }
}
