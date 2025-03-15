namespace TogglTrack.DAL.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
    }
}
