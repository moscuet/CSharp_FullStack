using System.Text.Json;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Review> _reviews;

        public ReviewRepository(EshopDbContext context)
        {
            _context = context;
            _reviews = _context.Reviews;
        }


        public async Task<Review> CreateAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Review> CreateWithImagesAsync(Review review, List<string> imageUrls)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Add the review first
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                Console.WriteLine($"\nReview saved with ID: {review.Id}\n");

                var images = imageUrls.Select(url => new ReviewImage
                {
                    ReviewId = review.Id,
                    Url = url
                }).ToList();

                await _context.AddRangeAsync(images);
                await _context.SaveChangesAsync();

                Console.WriteLine($"\nImages saved for review ID: {review.Id}\n");

                await transaction.CommitAsync();

                // Query to get all images connected with the review
                var savedImages = await _context.ReviewImages
                    .Where(i => i.ReviewId == review.Id)
                    .ToListAsync();

                Console.WriteLine($"\nSaved Images: {JsonSerializer.Serialize(savedImages)}\n");

                // Reload the review to include the images
                var updatedReview = await _context.Reviews
                    .Include(r => r.ReviewImages)
                    .FirstOrDefaultAsync(r => r.Id == review.Id);

                Console.WriteLine($"\nFrom review repo: created review: {JsonSerializer.Serialize(updatedReview)}\n");

                return updatedReview;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback the transaction in case of error
                throw new Exception("An error occurred while creating the review with images.", ex);
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
            var review = await _context.Reviews
                .Include(r => r.ReviewImages)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            Console.WriteLine($"Review: {JsonSerializer.Serialize(review)}");
            Console.WriteLine($"Images: {JsonSerializer.Serialize(review.ReviewImages)}");

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
            return await _reviews.Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
        {
            return await _reviews.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync(QueryOptions options)
        {
            var limit = options.Limit.HasValue ? options.Limit.Value.ToString() : "NULL";
            var offset = options.StartingAfter.HasValue ? options.StartingAfter.Value.ToString() : "NULL";
            var sortBy = string.IsNullOrWhiteSpace(options.SortBy?.ToString()) ? "NULL" : $"'{options.SortBy.ToString()}'";
            var sortOrder = string.IsNullOrWhiteSpace(options.SortOrder?.ToString()) ? "NULL" : $"'{options.SortOrder.ToString()}'";
            var searchKey = string.IsNullOrWhiteSpace(options.SearchKey) ? "NULL" : $"'{options.SearchKey}'";

            var sql = $"SELECT * FROM get_reviews({limit}, {offset}, {sortBy}, {sortOrder}, {searchKey})";

            var reviews = await _context.Reviews.FromSqlRaw(sql).ToListAsync();
            return reviews;
        }


    }
}
