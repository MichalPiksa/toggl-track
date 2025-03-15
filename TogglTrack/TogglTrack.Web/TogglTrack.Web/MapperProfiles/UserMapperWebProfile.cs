using AutoMapper;
using TogglTrack.API.Abstractions;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Web.MapperProfiles
{
    public class UserMapperWebProfile : Profile
    {
        public UserMapperWebProfile() 
        {
            CreateMap<UserDetailModel, CreateUserRequest>();
        }
    }
}
