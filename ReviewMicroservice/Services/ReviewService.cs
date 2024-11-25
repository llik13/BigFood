using MongoDB.Driver;
using ReviewMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewMicroservice.Services
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review> _reviews;

        public ReviewService(IMongoDatabase database)
        {
            _reviews = database.GetCollection<Review>("Reviews");
        }

        public async Task AddReview(Review review)
        {
            await _reviews.InsertOneAsync(review);
        }

        public async Task<List<Review>> GetReviews(int pageNumber, int pageSize, bool sortByRating = false)
        {
            var query = _reviews.Find(r => true);

            if (sortByRating)
            {
                query = query.SortByDescending(r => r.Rating);
            }
            else
            {
                query = query.SortByDescending(r => r.DateCreated);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<long> GetReviewCount()
        {
            return await _reviews.CountDocumentsAsync(r => true);
        }
    }
}
