using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Promotion
{
    public class PromotionDto
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class PromotionProfile : Profile
        {
            public PromotionProfile()
            {
                CreateMap<Domain.Entitites.Promotion, PromotionDto>().ReverseMap();
            }
        }
    }

}
