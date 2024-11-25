using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Orders.BLL.DTO.Requests;
using Orders.BLL.DTO.Responses;
using Orders.BLL.Grpc;
using Orders.DAL.Models;

namespace Orders.AutomapperProfile;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Order, ShortOrderResponse>().ReverseMap();
        CreateMap<Order, OrderResponse>().ReverseMap();
        CreateMap<Order, OrderRequest>();
        CreateMap<OrderRequest, Order>().ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Product, ShortProductResponse>().ReverseMap();

        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<Order, ShortOrderResponseGrpc>().ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => Timestamp.FromDateTime(src.OrderDate.ToUniversalTime())));
        
        CreateMap<ShortOrderResponseGrpc, Order>();
        CreateMap<ShortProductResponseGrpc, Product>().ReverseMap();
    }
    
}