using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.User.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UpdateUserHandler> _logger;

            public UpdateUserHandler(IApplicationDbContext context, IMapper mapper, ILogger<UpdateUserHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.User.FindAsync(new object[] { request.Id }, cancellationToken);

                if (user == null)
                {
                    _logger.LogError($"User with Id {request.Id} not found.");
                    throw new Exception($"User with Id {request.Id} not found.");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Number = request.Number;
                user.Email = request.Email;
                user.Address = request.Address;
                user.Role = request.Role;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
