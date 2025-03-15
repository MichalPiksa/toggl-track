using Microsoft.EntityFrameworkCore;

namespace TogglTrack.DAL.Repositories
{
    public class ProjectRepository : Repository<ProjectEntity>
    {
        public ProjectRepository(TogglTrackDbContext togglTrackDbContext) : base(togglTrackDbContext)
        {
        }
        public override Task<ProjectEntity?> GetByIdAsync(Guid id)
        {
            return togglTrackDbContext.Projects
                .Include(x => x.Users)
                .Include(y => y.Activities)
                .FirstOrDefaultAsync(z => z.Id == id);
        }
        public override IQueryable<ProjectEntity> GetAll()
        {
            return togglTrackDbContext.Projects.Include(x => x.Users);
        }
    }
}
