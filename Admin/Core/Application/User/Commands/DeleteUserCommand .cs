using Aplication.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.User.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger<DeleteUserHandler> _logger;

            public DeleteUserHandler(IApplicationDbContext context, ILogger<DeleteUserHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.User.FindAsync(new object[] { request.Id }, cancellationToken);

                if (user == null)
                {
                    _logger.LogError($"User with Id {request.Id} not found.");
                    throw new Exception($"User with Id {request.Id} not found.");
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }


}
