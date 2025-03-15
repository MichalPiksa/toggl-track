using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TogglTrack.DAL
{
    public class TogglTrackDbContextFactory
        : IDesignTimeDbContextFactory<TogglTrackDbContext>, IDbContextFactory<TogglTrackDbContext>
    {
        public TogglTrackDbContext CreateDbContext()
        {
            return CreateDbContext([]);
        }

        public TogglTrackDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<TogglTrackDbContextFactory>()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TogglTrackDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ToggleTrack"));

            return new TogglTrackDbContext(optionsBuilder.Options);
        }
    }
}
