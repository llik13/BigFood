using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewsAsync();
    }
}