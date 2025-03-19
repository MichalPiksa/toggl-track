using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.Common.Models.Project;
using TogglTrack.DAL;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.BL.Facades
{
    public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectFacade
    {
        private readonly IRepository<ActivityEntity> activityRepository;

        public ProjectFacade(IMapper mapper, IRepository<ProjectEntity> repository, IRepository<ActivityEntity> activityRepository) : base(mapper, repository)
        {
            this.activityRepository = activityRepository;
        }
        public async Task<ProjectDetailModel> CreateProjectAsync(string projectName)
        {
            var newProject = new ProjectEntity()
            {
                Id = Guid.NewGuid(),
                Name = projectName,
            };
            await repository.InsertAsync(newProject);
            return mapper.Map<ProjectDetailModel>(newProject);
        }

        public async Task<IEnumerable<ProjectListModel>> GetUserProjectsAsync(Guid userId)
        {
            var userProjects = await repository.GetAll().Where(p => p.Users.Any(u => u.Id == userId)).ToListAsync();
            return mapper.Map<IEnumerable<ProjectListModel>>(userProjects);
        }

        //public async Task<ProjectDetailModel?> ActiveProject(Guid userId)
        //{
        //    var test = activityRepository.GetAll().Where(a => a.UserId == userId && x => x.);
        //    var activeProject = repository.GetAll().Where(p => p.Users.Any(u => u.Id == userId)).Where(a => a.Activities.);
        //}
    }
}
