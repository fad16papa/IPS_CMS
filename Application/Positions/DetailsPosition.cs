using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Positions
{
    public class DetailsPosition
    {
        public class Query : IRequest<Position>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Position>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Position> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var position = await _context.Position.FindAsync(request.Id);

                if (position == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                return position;
            }
        }
    }
}