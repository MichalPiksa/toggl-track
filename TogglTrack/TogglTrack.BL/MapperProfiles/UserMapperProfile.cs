using AutoMapper;
using TogglTrack.Common.Models.User;
using TogglTrack.DAL;

namespace TogglTrack.BL.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserEntity, UserListModel>();
            CreateMap<UserEntity, UserDetailModel>();

            CreateMap<UserDetailModel, UserEntity>();
        }
    }
}
