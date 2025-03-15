using Microsoft.EntityFrameworkCore;

namespace TogglTrack.DAL.Tests
{
    public class DatabaseFixture<TEntity> : IDisposable where TEntity : class, IEntity
    {
        private readonly string _databaseName = Guid.NewGuid().ToString();
        public DatabaseFixture()
        {
            PrepareDatabase();
        }
        public void PrepareDatabase()
        {
            using (var dbContext = CreateDbContext()) 
            {
                dbContext.Database.EnsureCreated();
            }
        }

        public TogglTrackDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TogglTrackDbContext>();
            optionsBuilder.UseInMemoryDatabase(_databaseName);
            optionsBuilder.EnableSensitiveDataLogging(true);
            var dbContext = new TogglTrackDbContext(optionsBuilder.Options);
            return dbContext;
        }

        public void Dispose()
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.SaveChanges();
            }
        }
    }
}
