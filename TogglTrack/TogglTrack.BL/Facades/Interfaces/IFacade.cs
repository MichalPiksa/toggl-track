
using TogglTrack.Common.Models;

namespace TogglTrack.BL.Facades.Interfaces
{
    public interface IFacade<TListModel, TDetailModel> 
        where TListModel : IModel
        where TDetailModel : class, IModel
    {
        IEnumerable<TListModel> GetAll();
        Task<TDetailModel?> GetByIdAsync(Guid id);
        Task<TDetailModel> SaveAsync(TDetailModel model);
        Task DeleteAsync(Guid id);
    }
}
