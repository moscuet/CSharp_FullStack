
using System.Text.Json;
using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.ServiceAbstraction;
namespace Eshop.Service.src.Service
{
    public abstract class BaseService<TEntity, TCreateDTO, TUpdateDTO, TReadDTO> : IBaseService<TCreateDTO, TUpdateDTO, TReadDTO>
        where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TReadDTO> CreateAsync(TCreateDTO createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
             EntityValidator.ValidateEntity(entity); 
            var createdEntity = await _repository.CreateAsync(entity);
            var res =   _mapper.Map<TReadDTO>(createdEntity);
            return res;
        }




        public virtual async Task<bool> UpdateAsync(Guid id, TUpdateDTO updateDto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            _mapper.Map(updateDto, existingEntity);
            return await _repository.UpdateAsync(existingEntity);
        }

        public virtual async Task<TReadDTO> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            return _mapper.Map<TReadDTO>(entity);
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            return await _repository.DeleteByIdAsync(id);
        }
    }
}
