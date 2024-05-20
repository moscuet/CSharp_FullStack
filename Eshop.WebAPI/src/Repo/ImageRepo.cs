using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;


namespace Eshop.WebApi.src.Repo
{
    public class ImageRepository : IImageRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Image> _images;

        public ImageRepository(EshopDbContext context)
        {
            _context = context;
            _images = _context.Images;
        }

        public async Task<Image> CreateAsync(Image image)
        {
            await _images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> UpdateAsync(Image image)
        {
            var existingImage = await _images.AsNoTracking().FirstOrDefaultAsync(i => i.Id == image.Id);
            if (existingImage == null)
            {
                throw new KeyNotFoundException($"Image with ID {image.Id} not found.");
            }

            _context.Update(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Image> GetByIdAsync(Guid id)
        {
            var image = await _images.FirstOrDefaultAsync(i => i.Id == id);
            if (image == null)
            {
                throw new KeyNotFoundException($"Image with ID {id} not found.");
            }
            return image;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var image = await _images.FirstOrDefaultAsync(i => i.Id == id);
            if (image == null)
            {
                throw new KeyNotFoundException($"Image with ID {id} not found.");
            }

            _images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Image>> GetImagesByEntityIdAsync(Guid entityId)
        {
            return await _images.Where(i => i.EntityId == entityId).ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync(QueryOptions options)
        {
            return await _images.ToListAsync(); // Apply any options-based filtering here as needed
        }
    }
}
