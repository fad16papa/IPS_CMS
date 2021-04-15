using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserRole
{
    public class CreateUserRole
    {
        public class CommandCreateUserRole : IRequest
        {
            public AppUserRole AppUserRole { get; set; }
        }

        public class Handler : IRequestHandler<CommandCreateUserRole>
        {
            private readonly RoleManager<AppUserRole> _roleManager;

            public Handler(RoleManager<AppUserRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(CommandCreateUserRole request, CancellationToken cancellationToken)
            {
                //logic goes here
                bool check = await _roleManager.RoleExistsAsync(request.AppUserRole.Name);

                if (check)
                {
                    throw new RestException(HttpStatusCode.Conflict, string.Format("The role name {0} is already created", request.AppUserRole.Name));
                }

                var result = await _roleManager.CreateAsync(request.AppUserRole);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new Exception("Problem saving changes");
                }
            }
        }
    }
}