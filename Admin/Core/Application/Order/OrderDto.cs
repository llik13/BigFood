
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; }

        
       //public UserDto User { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entitites.Order, OrderDto>();
            }
        }
    }
}
