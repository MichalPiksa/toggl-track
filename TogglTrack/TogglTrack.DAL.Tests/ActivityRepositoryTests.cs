using TogglTrack.DAL.Repositories;

namespace TogglTrack.DAL.Tests
{
    public class ActivityRepositoryTests : IDisposable
    {
        private DatabaseFixture<ActivityEntity> fixture;

        public ActivityRepositoryTests()
        {
            fixture = new DatabaseFixture<ActivityEntity>();
        }
        [Fact]
        public void TestThatGetAllOnNewDbReturnsEmptyCollection()
        {
            // Arrange
            var repository = CreateRepository();

            // Act
            var allFromDb = repository.GetAll();

            // Assert
            Assert.Empty(allFromDb);
        }

        private Repository<ActivityEntity> CreateRepository()
        {
            return new Repository<ActivityEntity>(fixture.CreateDbContext());
        }

        [Fact]
        public void GetAllOnNonEmptyDbReturnsCorrectData()
        {
            // Arrange
            var repository = CreateRepository();
            var dbContext = fixture.CreateDbContext();
            var activity = new ActivityEntity()
            {
                ActivityType = "Test",
                Description = "Test"
            };

            dbContext.Activities.Add(activity);
            dbContext.SaveChanges();

            // Act
            var activities = repository.GetAll();

            // Assert
            Assert.Single(activities);
        }

        [Fact]
        public async Task RepositoryInsertShouldSaveToDbAsync()
        {
            // Arrange
            var repository = CreateRepository();
            var dbContext = fixture.CreateDbContext();
            var activity = new ActivityEntity()
            {
                ActivityType = "Test",
                Description = "Test"
            };

            // Act
            await repository.InsertAsync(activity);

            // Assert
            var activities = dbContext.Activities.ToList();
            Assert.Single(activities);
        }

        public void Dispose()
        {
            fixture.Dispose();
        }
    }
}
