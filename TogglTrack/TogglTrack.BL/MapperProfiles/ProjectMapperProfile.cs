using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TogglTrack.Common.Models.Project;
using TogglTrack.DAL;

namespace TogglTrack.BL.MapperProfiles
{
    public class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<ProjectEntity, ProjectListModel>();
            CreateMap<ProjectEntity, ProjectDetailModel>();

            CreateMap<ProjectDetailModel, ProjectEntity>();
        }
    }
}
