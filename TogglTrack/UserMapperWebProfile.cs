using System;
using AutoMapper;

namespace TogglTrack.Web.MapperProfiles
{
    public class UserWebMapperProfile : Profile
    {
        public UserWebMapperProfile()
        {
            CreateMap<UserDetailModel, CreateUserRequest>();
        }
    }
}
