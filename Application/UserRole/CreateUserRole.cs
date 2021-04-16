using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserRole
{
    public class CreateUserRole
    {
        public class CommandCreateUserRole : IRequest<Role>
        {
            public string Name { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class CommandValidator : AbstractValidator<CommandCreateUserRole>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.DateCreated).NotEmpty();
                RuleFor(x => x.IsEnable).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CommandCreateUserRole, Role>
        {
            private readonly RoleManager<AppUserRole> _roleManager;

            public Handler(RoleManager<AppUserRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<Role> Handle(CommandCreateUserRole request, CancellationToken cancellationToken)
            {
                //logic goes here
                bool check = await _roleManager.RoleExistsAsync(request.Name);

                if (check)
                {
                    throw new RestException(HttpStatusCode.Conflict, string.Format("The role name {0} is already created", request.Name));
                }

                var appUserRole = new AppUserRole
                {
                    Name = request.Name,
                    DateCreated = request.DateCreated,
                    IsEnable = request.IsEnable
                };

                var result = await _roleManager.CreateAsync(appUserRole);

                if (result.Succeeded)
                {
                    return new Role
                    {
                        Name = appUserRole.Name,
                        DateCreated = appUserRole.DateCreated,
                        IsEnable = appUserRole.IsEnable
                    };
                }
                else
                {
                    throw new Exception("Problem saving changes");
                }
            }
        }
    }
}