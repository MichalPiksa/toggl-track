using TogglTrack.Common.Models.User;

namespace TogglTrack.BL.Facades.Interfaces
{
    public interface IUserFacade : IFacade<UserListModel, UserDetailModel>
    {
        Task<UserDetailModel> CreateUserAsync(string firstName, string lastName, string photoUrl);
    }
}
