using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Positions
{
    public class ListPosition
    {
        public class Query : IRequest<List<Position>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Position>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Position>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var listPosition = await _context.Position.ToListAsync();

                return listPosition;
            }
        }
    }
}