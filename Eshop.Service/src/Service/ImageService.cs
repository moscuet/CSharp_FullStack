using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.ServiceAbstraction;


namespace Eshop.Service.src.Service
{
    public class ImageService : BaseService<Image, ImageCreateDTO, ImageUpdateDTO, ImageReadDTO>, IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
            : base(imageRepository, mapper)
        {
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<ImageReadDTO>> GetImagesByEntityIdAsync(Guid entityId)
        {
            var images = await _imageRepository.GetImagesByEntityIdAsync(entityId);
            return _mapper.Map<IEnumerable<ImageReadDTO>>(images);
        }

        public async Task<IEnumerable<ImageReadDTO>> GetAllImagesAsync(QueryOptions options)
        {
            var images = await _imageRepository.GetAllImagesAsync(options);
            return _mapper.Map<IEnumerable<ImageReadDTO>>(images);
        }
    }
}