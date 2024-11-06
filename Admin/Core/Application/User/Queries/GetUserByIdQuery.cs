using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.User.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
        
        
        public GetUserByIdQuery(int Id) 
        {
            this.Id = Id;
        }

        public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetUserByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var user = await _context.User.FindAsync(query.Id);
                if (user == null)
                {
                    return null;
                }
                return _mapper.Map<UserDto>(user);
            }
        }
    }
}
