using AutoMapper;
using Catalog.BuisnesDataLayer.DTO.Response;
using Catalog.BuisnesDataLayer.Interfaces;
using Catalog.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReviewResponseDTO>> GetReviewsAsync()
        {
            var reviews = await unitOfWork.ReviewRepository.GetAllAsync();
            var reviwsDTOs =  mapper.Map<IEnumerable<ReviewResponseDTO>>(reviews);
            return reviwsDTOs;
        }
    }
}
