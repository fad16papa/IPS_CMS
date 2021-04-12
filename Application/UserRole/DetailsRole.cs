using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserRole
{
    public class DetailsRole
    {
        public class Query : IRequest<AppUserRole>
        {
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Query, AppUserRole>
        {
            private readonly RoleManager<AppUserRole> _roleManager;

            public Handler(RoleManager<AppUserRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<AppUserRole> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var appUser = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == request.Name);

                if (appUser == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                return appUser;
            }
        }
    }
}