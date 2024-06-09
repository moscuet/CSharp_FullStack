using System.Text.Json;
using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class ProductService : BaseService<Product, ProductCreateDTO, ProductUpdateDTO, ProductReadDTO>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
            : base(productRepository, mapper)
        {
            _productRepository = productRepository;
        }


        public async Task<Product> ProductCreateAsync(ProductCreateDTO productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            var createdProduct = await _productRepository.CreateWithImagesAsync(product, productCreateDto.ImageUrls);
            return createdProduct;
        }

        public async Task<ProductQueryReadDTO> GetAllProductsAsync(QueryOptions? options)
        {
            int currentPage = (options.StartingAfter ?? 0) / (options.Limit ?? 20) + 1;
            var (products, totalPages) = await _productRepository.GetAllProductsAsync(options);
            var mappedProducts = _mapper.Map<IEnumerable<ProductReadDTO>>(products);

            return new ProductQueryReadDTO
            {
                Products = mappedProducts,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };
        }


        public async Task<Product> UpdateProductWithImagesAsync(Guid id, ProductUpdateDTO productUpdateDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            _mapper.Map(productUpdateDto, existingProduct);
            var updatedProduct = await _productRepository.UpdateWithImageAsync(existingProduct, productUpdateDto.ImageUrls);
            return updatedProduct;
        }
    }
}
