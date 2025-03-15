using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TogglTrack.Common.Models.Activity;
using TogglTrack.DAL;

namespace TogglTrack.BL.MapperProfiles
{
    public class ActivityMapperProfile : Profile
    {
        public ActivityMapperProfile()
        {
            CreateMap<ActivityEntity, ActivityListModel>();
            CreateMap<ActivityListModel, ActivityEntity>()
                .ForMember(destination => destination.User, expression => expression.Ignore())
                .ForMember(destination => destination.Project, expression => expression.Ignore());

            CreateMap<ActivityEntity, ActivityDetailModel>();
            CreateMap<ActivityDetailModel, ActivityEntity>()
                .ForMember(destination => destination.UserId, expression => expression.MapFrom(model => model.User!.Id))
                .ForMember(destination => destination.User, expression => expression.Ignore())
                .ForMember(destination => destination.ProjectId, expression => expression.MapFrom(model => model.Project!.Id))
                .ForMember(destination => destination.Project, expression => expression.Ignore());
        }
    }
}
