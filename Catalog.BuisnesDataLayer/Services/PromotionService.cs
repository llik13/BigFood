using AutoMapper;
using Catalog.BuisnesDataLayer.Interfaces;
using Catalog.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public PromotionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddPtomotionToProductAsync(int promotionId, int productId)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
            var promotion = await unitOfWork.PromotionRepository.GetByIdAsync(promotionId);
            product.Promotions.Add(promotion);
            await unitOfWork.CompleteAsync();
        }
        public async Task DeletePtomotionRomProductAsync(int promotionId, int productId)
        {
            var product = await unitOfWork.ProductRepository.GetProdByIdWithPromAsync(productId);
            var productPromotion = product.Promotions.FirstOrDefault(t => t.PromotionId == promotionId);
            product.Promotions.Remove(productPromotion);
            await unitOfWork.CompleteAsync();
        }
    }
}
