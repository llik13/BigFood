using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Category
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entitites.Category, CategoryDto>().ReverseMap();
            }
        }
    }
}
