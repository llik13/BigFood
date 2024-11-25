using AutoMapper;
using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.BuisnesDataLayer.DTO.Response;
using Catalog.BuisnesDataLayer.Interfaces;
using Catalog.BuisnesDataLayer.Validation;
using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;
using FluentValidation;
using Catalog.DataAccessLayer.Pagination.Parametrs;

namespace Catalog.BuisnesDataLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductFullResponseDTO>> GetFullProductsInformationAsync(ProductParametrs productParametrs)
        {
            var products = await unitOfWork.ProductRepository.GetProductsAsync(productParametrs);
            var productDTOs = mapper.Map<IEnumerable<ProductFullResponseDTO>>(products);
            return productDTOs;
        }

        public async Task<IEnumerable<ProductSummaryResponseDTO>> GetProductSummaryInformationAsync(ProductParametrs productParametrs)
        {
            var products = await unitOfWork.ProductRepository.GetProductsAsync(productParametrs);
            var producrDTOs = mapper.Map<IEnumerable<ProductSummaryResponseDTO>>(products);
            return producrDTOs;
        }

        public async Task AddProductAsync(ProductRequestDTO productDTO)
        {
            /*
            var validator = new ProductValidation();
            var result = await validator.ValidateAsync(productDTO);
            if (!result.IsValid) 
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Property: {error.PropertyName} Error: {error.ErrorMessage}");
                }
                throw new ValidationException(result.Errors);
            }
            */

            var product = mapper.Map<Product>(productDTO); 
            await unitOfWork.ProductRepository.AddAsync(product);
            await unitOfWork.CompleteAsync();
        }

        public async Task RemoveProductAsync(int id)
        {
            
            await unitOfWork.ProductRepository.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
        }


        public async Task UpdateProductAsync(ProductRequestDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            await unitOfWork.ProductRepository.ReplaceAsync(product);
            await unitOfWork.CompleteAsync();
        }
    }
}
