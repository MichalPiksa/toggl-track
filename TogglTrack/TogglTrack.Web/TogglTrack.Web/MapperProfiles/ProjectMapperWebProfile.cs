using AutoMapper;
using TogglTrack.API.Abstractions;
using TogglTrack.Common.Models.Project;

namespace TogglTrack.Web.MapperProfiles
{
    public class ProjectMapperWebProfile : Profile
    {
        public ProjectMapperWebProfile()
        {
            CreateMap<ProjectDetailModel, CreateProjectRequest>();
            //CreateMap<CreateProjectRequest, ProjectDetailModel>();
        }
    }
}
