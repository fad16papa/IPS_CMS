using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserRole
{
    public class CreateUserRole
    {
        public class CommandCreateUserRole : IRequest
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