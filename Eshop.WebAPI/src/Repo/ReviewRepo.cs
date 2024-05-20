using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Core.src.ValueObject;
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



        public async Task<Review> CreateAsync(Review review)
    {
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                foreach (var image in review.Images)
                {
                    image.EntityId = review.Id;
                    await _context.Images.AddAsync(image);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return review;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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
            var review = await _reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null)
            {
                throw AppException.NotFound($"Review with ID {id} not found.");
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
