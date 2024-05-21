using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Eshop.WebApi.src.Repo
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Review> _reviews;
                private readonly DbSet<ReviewImage> _images;


        public ReviewRepository(EshopDbContext context)
        {
            _context = context;
            _reviews = _context.Reviews;
            _images = _context.ReviewImages;
        }


        public async Task<Review> CreateAsync(Review entity)
        {
            await _reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<Review> CreateWithImagesAsync(Review review, List<string> urls)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                if (urls != null && urls.Count > 0)
                {
                    var reviewImages = urls.Select(imageUrl => new ReviewImage
                    {
                        ReviewId = review.Id,
                        Url = imageUrl
                    }).ToList();

                    await _images.AddRangeAsync(reviewImages);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return review;
            }
            catch (Exception ex)
            {
                if (transaction.GetDbTransaction().Connection != null)
                {
                    await transaction.RollbackAsync();
                }
                throw new Exception("An error occurred while creating the review.", ex);
            }
        }



        public async Task<bool> UpdateAsync(Review review)
        {
            var existingReview = await _reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == review.Id);
            if (existingReview == null)
            {
                throw AppException.NotFound($"Review with ID {review.Id} not found.");
            }
            _context.Update(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            var review = await _reviews
                .Include(r => r.ReviewImages)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }
            return review;
        }


        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var review = await _reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null)
            {
                throw AppException.NotFound($"Review with ID {id} not found.");
            }

            _reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId)
        {
            return await _reviews.Include(r => r.ReviewImages).Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
        {
            return await _reviews.Include(r => r.ReviewImages).Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync(QueryOptions options)
        {
            var limit = options.Limit.HasValue ? options.Limit.Value.ToString() : "NULL";
            var offset = options.StartingAfter.HasValue ? options.StartingAfter.Value.ToString() : "NULL";
            var sortBy = string.IsNullOrWhiteSpace(options.SortBy?.ToString()) ? "NULL" : $"'{options.SortBy.ToString()}'";
            var sortOrder = string.IsNullOrWhiteSpace(options.SortOrder?.ToString()) ? "NULL" : $"'{options.SortOrder.ToString()}'";
            var searchKey = string.IsNullOrWhiteSpace(options.SearchKey) ? "NULL" : $"'{options.SearchKey}'";

            var sql = $"SELECT * FROM get_reviews({limit}, {offset}, {sortBy}, {sortOrder}, {searchKey})";

            var reviews = await _reviews.FromSqlRaw(sql).Include(r => r.ReviewImages).ToListAsync();
            
            return reviews;
        }
    }
}

