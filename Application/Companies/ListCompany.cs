using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Companies
{
    public class ListCompany
    {
        public class Query : IRequest<List<Company>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Company>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Company>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var listCompany = await _context.Company.ToListAsync();

                return listCompany;
            }
        }
    }
}