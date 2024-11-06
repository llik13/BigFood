using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.User.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetUsersHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<List<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
            {
                var users = await _context.User.ToListAsync(cancellationToken);
                return _mapper.Map<List<UserDto>>(users);
            }
        }
    }
}
