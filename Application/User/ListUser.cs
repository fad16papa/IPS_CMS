using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User
{
    public class ListUser
    {
        public class Query : IRequest<List<AppUser>>
        {

        }

        public class Handler : IRequestHandler<Query, List<AppUser>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<AppUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var appuser = await _context.Users.ToListAsync();

                return appuser;
            }
        }
    }
}
