
namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IBaseService<TCreateDTO, TUpdateDTO, TReadDTO>
    {
        Task<TReadDTO> CreateAsync(TCreateDTO createDto);
        Task<bool> UpdateAsync(Guid id, TUpdateDTO updateDto);
        Task<TReadDTO> GetByIdAsync(Guid id);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
