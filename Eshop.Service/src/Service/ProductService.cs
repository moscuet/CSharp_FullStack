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


      public  async Task<Product>  ProductCreateAsync(ProductCreateDTO productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            var createdProduct = await _productRepository.CreateWithImagesAsync(product, productCreateDto.ImageUrls);
            return createdProduct;
        }

        public async Task<IEnumerable<ProductReadDTO>> GetAllProductsAsync(QueryOptions? options)
        {
            var products = await _productRepository.GetAllProductsAsync(options);
            return _mapper.Map<IEnumerable<ProductReadDTO>>(products);
        }

        public async Task<Product> UpdateProductWithImagesAsync(Guid id, ProductUpdateDTO productUpdateDto)
        {
            Console.WriteLine(JsonSerializer.Serialize(productUpdateDto));

                Console.WriteLine("");

            var existingProduct = await _productRepository.GetByIdAsync(id);
                Console.WriteLine(JsonSerializer.Serialize(existingProduct));

            if (existingProduct  == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
                Console.WriteLine("");

            _mapper.Map(productUpdateDto, existingProduct);
            Console.WriteLine(JsonSerializer.Serialize(existingProduct));

            // var product = _mapper.Map<Product>(productUpdateDto);
            var updatedProduct = await _productRepository.UpdateWithImageAsync(existingProduct, productUpdateDto.ImageUrls);
            return updatedProduct;
        }

    }
}
