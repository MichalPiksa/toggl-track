using Microsoft.EntityFrameworkCore;

namespace TogglTrack.DAL.Repositories
{
    public class UserRepository : Repository<UserEntity>
    {
        public UserRepository(TogglTrackDbContext togglTrackDbContext) : base(togglTrackDbContext)
        {
        }

        public override Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return togglTrackDbContext.Users
                .Include(x => x.Activities)
                .Include(y => y.Projects)
                .FirstOrDefaultAsync(z => z.Id == id);
        }
        public override IQueryable<UserEntity> GetAll()
        {
            return togglTrackDbContext.Users.Include(x => x.Activities);
        }
    }
}
