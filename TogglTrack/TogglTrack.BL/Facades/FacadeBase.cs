using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.Common.Models;
using TogglTrack.DAL;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.BL.Facades
{
    public abstract class FacadeBase<TEntity, TListModel, TDetailModel> : IFacade<TListModel, TDetailModel>
        where TEntity : class, IEntity
        where TListModel : class, IModel
        where TDetailModel : class, IModel
    {
        protected readonly IMapper mapper;
        protected readonly IRepository<TEntity> repository;

        protected FacadeBase(IMapper mapper, IRepository<TEntity> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task DeleteAsync(Guid id)
        {
            var isExist = await repository.ExistsAsync(id);
            if (isExist)
            {
                await repository.DeleteAsync(id);
            }
            else
            {
                throw new ArgumentException($"Item with id {id} does not exist", nameof(id));
            }
        }

        public IEnumerable<TListModel> GetAll()
        {
            var entities = repository.GetAll();
            return mapper.Map<IEnumerable<TListModel>>(entities);
        }

        public async Task<TDetailModel?> GetByIdAsync(Guid id)
        {
            var entity = await repository.GetByIdAsync(id);
            return entity is null ? null : mapper.Map<TDetailModel>(entity);
        }

        public async Task<TDetailModel> SaveAsync(TDetailModel model)
        {
            var entity = mapper.Map<TEntity>(model);
            var operation = await repository.ExistsAsync(model.Id) ? repository.UpdateAsync(entity) : repository.InsertAsync(entity);
            var result = await operation;
            return mapper.Map<TDetailModel>(result);
        }
    }
}
