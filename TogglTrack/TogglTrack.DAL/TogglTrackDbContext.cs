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
        }
    }
}
