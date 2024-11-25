using CourierService.Application.Orders.Queries.GetOrderDetails;
using CourierService.Domain.Entities;

namespace CourierService.Application.Common.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Order, OrderDetailsDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User!.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User!.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User!.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User!.Phone));
    }
        
}
