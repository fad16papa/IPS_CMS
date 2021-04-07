using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Departments
{
    public class DetailsDepartment
    {
        public class Query : IRequest<Department>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Department>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Department> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var deparment = await _context.Department.FindAsync(request.Id);

                if (deparment == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                return deparment;
            }
        }
    }
}