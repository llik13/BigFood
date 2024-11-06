using AutoMapper;
using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.BuisnesDataLayer.DTO.Response;
using Catalog.DataAccessLayer.Entities;

namespace Catalog.BuisnesDataLayer
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile() 
        {
            CreateMap<Product, ProductFullResponseDTO>()
            .ForMember(dest => dest.Promotion, opt => opt.MapFrom(src => src.Promotions.FirstOrDefault()))  
            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Producttags.FirstOrDefault()));

            CreateMap<Promotion, PromotionResponseDTO>();

            CreateMap<Producttag, TagResponseDTO>();

            CreateMap<Product, ProductSummaryResponseDTO>();

            CreateMap<ProductRequestDTO, Product>();

            CreateMap<Category, CategoryResponseDTO>();

            CreateMap<Review, ReviewResponseDTO>();

            CreateMap<CategoryRequestDTO, Category>();
        }
    }
}
