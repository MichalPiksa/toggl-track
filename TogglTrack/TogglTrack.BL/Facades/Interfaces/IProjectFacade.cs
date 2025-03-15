using TogglTrack.Common.Models.Project;

namespace TogglTrack.BL.Facades.Interfaces
{
    public interface IProjectFacade : IFacade<ProjectListModel, ProjectDetailModel>
    {
        Task<ProjectDetailModel> CreateProjectAsync(string projectName);
    }
}
