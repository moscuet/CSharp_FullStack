using Eshop.Core.src.Common;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IImageService : IBaseService<ImageCreateDTO, ImageUpdateDTO, ImageReadDTO>
    {
        Task<IEnumerable<ImageReadDTO>> GetImagesByEntityIdAsync(Guid entityId);
        Task<IEnumerable<ImageReadDTO>> GetAllImagesAsync(QueryOptions options);
    }
}
