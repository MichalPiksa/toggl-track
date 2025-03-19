using Microsoft.EntityFrameworkCore;

namespace TogglTrack.DAL.Repositories
{
    public class ActivityRepository : Repository<ActivityEntity>
    {
        public ActivityRepository(TogglTrackDbContext togglTrackDbContext) : base(togglTrackDbContext)
        {
        }
        public override Task<ActivityEntity?> GetByIdAsync(Guid id)
        {
            return togglTrackDbContext.Activities
                .Include(x => x.User)
                .Include(y => y.Project)
                .FirstOrDefaultAsync(z => z.Id == id);
        }
        public override IQueryable<ActivityEntity> GetAll()
        {
            return togglTrackDbContext.Activities
                .Include(x => x.User)
                .Include(y => y.Project);
        }
    }
}
