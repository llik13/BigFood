using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag
{
    public class ProductTagDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;

        public class Mapping : Profile
        {
            public Mapping() 
            {
                CreateMap<Domain.Entitites.ProductTag, ProductTagDto>();
            }

        }
    }

}
