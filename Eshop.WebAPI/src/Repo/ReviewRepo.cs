using System.Text.Json;
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


     public async Task<Review> CreateWithImagesAsync(Review review, List<string> urls)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        // Add the review
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        // Add images associated with the review
        if (urls != null && urls.Count > 0)
        {
            var reviewImages = urls.Select(imageUrl => new ReviewImage
            {
                ReviewId = review.Id,
                Url = imageUrl
            }).ToList();

            await _context.ReviewImages.AddRangeAsync(reviewImages);
            await _context.SaveChangesAsync();
        }

        // Commit transaction
        await transaction.CommitAsync();

        // Return the review
        return review;
    }
    catch (Exception ex)
    {
        // Attempt rollback if transaction is still active
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

        public Task<IEnumerable<Review>> GetAllReviewsAsync(QueryOptions options)
        {
            throw new NotImplementedException();
        }

        // public async Task<IEnumerable<Review>> GetAllReviewsAsync(QueryOptions options)
        // {
        //     var limit = options.Limit.HasValue ? options.Limit.Value.ToString() : "NULL";
        //     var offset = options.StartingAfter.HasValue ? options.StartingAfter.Value.ToString() : "NULL";
        //     var sortBy = string.IsNullOrWhiteSpace(options.SortBy?.ToString()) ? "NULL" : $"'{options.SortBy.ToString()}'";
        //     var sortOrder = string.IsNullOrWhiteSpace(options.SortOrder?.ToString()) ? "NULL" : $"'{options.SortOrder.ToString()}'";
        //     var searchKey = string.IsNullOrWhiteSpace(options.SearchKey) ? "NULL" : $"'{options.SearchKey}'";

        //     var sql = $"SELECT * FROM get_reviews({limit}, {offset}, {sortBy}, {sortOrder}, {searchKey})";

        //     var reviews = await _context.Reviews.FromSqlRaw(sql).ToListAsync();
        //     return reviews;
        // }


    }
}
