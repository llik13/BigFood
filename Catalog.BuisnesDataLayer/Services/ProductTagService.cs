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
    public class ProductTagService : IProductTagService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public ProductTagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddTagToProductAsync(int productId, int tagId)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
            var tag = await unitOfWork.ProducttagRepository.GetByIdAsync(tagId);


                product.Producttags.Add(tag);
                await unitOfWork.CompleteAsync();
        }
        public async Task DeleteTagFromProductAsync(int productId, int tagId)
        {
            var product = await unitOfWork.ProductRepository.GetProdByIdWithTagAsync(productId);
            var productTag = product.Producttags.FirstOrDefault(t => t.TagId == tagId);

            product.Producttags.Remove(productTag);
            await unitOfWork.CompleteAsync();
        }
    }
}
