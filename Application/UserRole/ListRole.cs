using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRole
{
    public class ListRole
    {
        public class Query : IRequest<List<AppUserRole>>
        {

        }

        public class Handler : IRequestHandler<Query, List<AppUserRole>>
        {
            private readonly RoleManager<AppUserRole> _roleManager;
            public Handler(RoleManager<AppUserRole> roleManager)
            {
                _roleManager = roleManager;

            }

            public async Task<List<AppUserRole>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var listAppUserRole = await _roleManager.Roles.ToListAsync();

                return listAppUserRole;
            }
        }
    }
}