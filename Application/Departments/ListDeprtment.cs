using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Departments
{
    public class ListDeprtment
    {
        public class Query : IRequest<List<Department>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Department>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Department>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var listDepartment = await _context.Department.ToListAsync();

                return listDepartment;
            }
        }
    }
}