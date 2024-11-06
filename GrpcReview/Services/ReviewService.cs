using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcReview.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcReview.Services
{
    public class ReviewService : Reviews.ReviewsBase
    {

        private readonly ReviewContext _dbContext;

        public ReviewService(ReviewContext context)
        {
            _dbContext = context;
        }

        public override async Task<ReviewList> GetReviews(Empty request, ServerCallContext context)
        {
            var reviews = await _dbContext.Reviews.ToListAsync();

            var reviewProtobuf = new ReviewList();

            reviewProtobuf.Reviews.AddRange(reviews.Select(c => new Protos.Review
            {
                Id = c.Id,
                UserId = c.UserId,
                Content = c.Content,
                Rating = ByteString.CopyFrom(new byte[] { c.Rating }),
                CreatedAt = Timestamp.FromDateTime(c.CreatedAt.ToUniversalTime())
            }));

            return reviewProtobuf;

        }

        public override async Task<Protos.Review> GetById(ReviewIdRequest request, ServerCallContext context)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == request.Id);

            if (review == null)
            {
                return null;
            }

            var reviewProtobuf = new Protos.Review
            {
                Id = review.Id,
                UserId = review.UserId,
                Content = review.Content,
                Rating = ByteString.CopyFrom(new byte[] { review.Rating }),
                CreatedAt = Timestamp.FromDateTime(review.CreatedAt.ToUniversalTime())
            };

            return reviewProtobuf;
        }

        public override async Task<Empty> AddReview(Protos.Review request, ServerCallContext context)
        {
            var review = new Review
            {
                Id = request.Id,
                UserId = request.UserId,
                Content = request.Content,
                Rating = request.Rating.ToByteArray().FirstOrDefault(),
                CreatedAt = request.CreatedAt.ToDateTime()
            };

            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<Empty> UpdateReviews(Protos.Review request, ServerCallContext context)
        {
            var review = new Review
            {
                Id = request.Id,
                UserId = request.UserId,
                Content = request.Content,
                Rating = request.Rating.ToByteArray().FirstOrDefault(),
                CreatedAt = request.CreatedAt.ToDateTime()
            };

            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<Empty> DeleteReviews(Protos.Review request, ServerCallContext context)
        {
            var review = new Review
            {
                Id = request.Id,
                UserId = request.UserId,
                Content = request.Content,
                Rating = request.Rating.ToByteArray().FirstOrDefault(),
                CreatedAt = request.CreatedAt.ToDateTime()
            };

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

    }
}
