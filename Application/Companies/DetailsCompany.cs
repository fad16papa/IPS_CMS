using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Companies
{
    public class DetailsCompany
    {
        public class Query : IRequest<Company>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Company>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Company> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var company = await _context.Company.FindAsync(request.Id);

                if (company == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                return company;
            }
        }
    }
}