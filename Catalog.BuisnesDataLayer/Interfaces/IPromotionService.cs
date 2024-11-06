using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Interfaces
{
    public interface IPromotionService
    {
        Task AddPtomotionToProductAsync(int promotionId, int productId);
        Task DeletePtomotionRomProductAsync(int promotionId, int productId);
    }
}
