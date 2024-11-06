using AutoMapper;
using Map.Entities;
using Maps.Repositories.DTO;

namespace Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Delivery, DeliveryWithDeliverDTO>()
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.Deliver.firstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Deliver.lastName))
                .ForMember(dest => dest.number, opt => opt.MapFrom(src => src.Deliver.number));
        }
    }
}
