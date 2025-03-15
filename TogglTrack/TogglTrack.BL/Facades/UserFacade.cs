using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.User;
using TogglTrack.DAL;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.BL.Facades
{
    public class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel>, IUserFacade
    {

        public UserFacade(IMapper mapper, IRepository<UserEntity> repository) : base(mapper, repository)
        {
        }
        public async Task<UserDetailModel> CreateUserAsync(string firstName, string lastName, string photoUrl)
        {
            var newUser = new UserEntity()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                PhotoUrl = photoUrl
            };
            await repository.InsertAsync(newUser);
            return mapper.Map<UserDetailModel>(newUser);
        }
    }
}
