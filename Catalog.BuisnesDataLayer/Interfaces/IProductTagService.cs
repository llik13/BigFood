using Catalog.BuisnesDataLayer.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Interfaces
{
    public interface IProductTagService
    {
        Task AddTagToProductAsync(int productId, int tagId);
        Task DeleteTagFromProductAsync(int productId, int tagId);

    }
}
