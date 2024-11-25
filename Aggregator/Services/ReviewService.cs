using Aggregator.Extensions;
using Aggregator.Models;

namespace Aggregator.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _client;
        public ReviewService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsAsync()
        {
            var response = await _client.GetAsync("/api/review");
            response.EnsureSuccessStatusCode();
            return await response.ReadContentAs<List<ReviewDTO>>();
        }
    }
}
