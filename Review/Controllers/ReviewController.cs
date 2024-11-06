using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Review.Entitites;
using Review.Interfaces;

namespace Review.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReview(int id)
        {
            var review = await _unitOfWork._reviewRepository.GetAsync(id);
            return review != null ? Ok(review) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetAllReviews()
        {
            var reviews = await _unitOfWork._reviewRepository.GetAllAsync();
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] Reviews review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork._reviewRepository.AddAsync(review);
            _unitOfWork.Commit();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, [FromBody] Reviews review)
        {
            if (id != review.Id || !ModelState.IsValid)
                return BadRequest();

            await _unitOfWork._reviewRepository.ReplaceAsync(review);
            _unitOfWork.Commit();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            await _unitOfWork._reviewRepository.DeleteAsync(id);
            _unitOfWork.Commit();
            return NoContent();
        }
    }
}
