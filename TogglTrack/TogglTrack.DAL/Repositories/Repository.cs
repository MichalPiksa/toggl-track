using Microsoft.EntityFrameworkCore;

namespace TogglTrack.DAL.Repositories
{

    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly TogglTrackDbContext togglTrackDbContext;

        public Repository(TogglTrackDbContext togglTrackDbContext)
        {
            this.togglTrackDbContext = togglTrackDbContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            return togglTrackDbContext.Set<T>();
        }
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            var result = await togglTrackDbContext.Set<T>().FindAsync(id);
            if (result == null)
            {
                throw new ArgumentException($"Item with id {id} not exist.");
            }
            return result;
        }
        public async Task<T> InsertAsync(T item)
        {
            if (await ExistsAsync(item.Id))
            {
                throw new ArgumentException("Item already exist", nameof(item));
            }
            else
            {
                var result = await togglTrackDbContext.Set<T>().AddAsync(item);
                await togglTrackDbContext.SaveChangesAsync();
                return result.Entity;
            }
        }
        public virtual async Task<T> UpdateAsync(T item)
        {
            if (await ExistsAsync(item.Id))
            {
                var result = togglTrackDbContext.Set<T>().Update(item);
                await togglTrackDbContext.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("Item not exist", nameof(item));
            }
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var existingItem = togglTrackDbContext.Set<T>().Single(item => item.Id == id);
            if (existingItem != null)
            {
                togglTrackDbContext.Set<T>().Remove(existingItem);
                await togglTrackDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Item with id {id} not exist", nameof(id));
            }
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await togglTrackDbContext.Set<T>().AnyAsync(item => item.Id == id);
        }
    }
}
