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
        public ProjectFacade(IMapper mapper, IRepository<ProjectEntity> repository) : base(mapper, repository)
        {
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
    }
}
