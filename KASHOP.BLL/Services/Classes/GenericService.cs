using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Classes;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity>
        where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }


        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericRepository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return 0;
            return _genericRepository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entities = _genericRepository.GetAll();
            if (onlyActive)
            {
                entities = entities.Where(e => e.Status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            _genericRepository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return 0;

            var updatedEntity = request.Adapt(entity);
            return _genericRepository.Update(updatedEntity);
        }
    }
}
