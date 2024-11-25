using Catalog.BuisnesDataLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDTO>> GetReviewsAsync();
    }
}
