using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public double? Rating { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Availability { get; set; }

        public class ProductProfile : Profile
        {
            public ProductProfile()
            {
                CreateMap<Domain.Entitites.Product, ProductDto>().ReverseMap();
            }
        }
    }
}
