using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Ingridient.Queries
{
    public class GetIngredientByIdQuery : IRequest<IngredientDto>
    {
        public int IngredientID { get; set; }

        public GetIngredientByIdQuery(int IngridientID) 
        {
            this.IngredientID = IngridientID;
        }

        public class GetIngredientByIdHandler : IRequestHandler<GetIngredientByIdQuery, IngredientDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIngredientByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IngredientDto> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
            {
                var ingredient = await _context.Ingridient.FindAsync(new object[] { request.IngredientID }, cancellationToken);
                return _mapper.Map<IngredientDto>(ingredient);
            }
        }
    }

}
