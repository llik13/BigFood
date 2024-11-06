using Aplication.Order;
using AutoMapper;
using Domain.Entitites;

namespace Aplication.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool IsBlocked { get; set; }


        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entitites.User, UserDto>();
            }
        }
    }
}
