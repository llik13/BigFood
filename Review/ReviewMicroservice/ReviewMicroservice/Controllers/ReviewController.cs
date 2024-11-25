using Microsoft.AspNetCore.Mvc;
using ReviewMicroservice.Models;
using ReviewMicroservice.Services;
using System.Threading.Tasks;

namespace ReviewMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (review == null || string.IsNullOrWhiteSpace(review.Title) || string.IsNullOrWhiteSpace(review.Content))
            {
                return BadRequest("Заголовок і зміст відгуку обов'язкові!");
            }

            if (review.Rating < 1 || review.Rating > 5)
            {
                return BadRequest("Оцінка має бути від 1 до 5!");
            }

            await _reviewService.AddReview(review);
            return CreatedAtAction(nameof(GetReviews), new { id = review.Id }, review);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(int pageNumber = 1, int pageSize = 10, bool sortByRating = false)
        {
            var reviews = await _reviewService.GetReviews(pageNumber, pageSize, sortByRating);
            return Ok(reviews);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetReviewCount()
        {
            var count = await _reviewService.GetReviewCount();
            return Ok(count);
        }
    }
}

