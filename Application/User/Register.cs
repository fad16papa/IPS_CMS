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
namespace Application.User
{
    public class Register
    {
        public class CommandRegisterUser : IRequest<User>
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string DisplayName { get; set; }
            public Guid DepartmentId { get; set; }
            public Guid PositionId { get; set; }
            public Guid CompanyId { get; set; }
        }

        public class CommandValidator : AbstractValidator<CommandRegisterUser>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.DepartmentId).NotEmpty();
                RuleFor(x => x.PositionId).NotEmpty();
                RuleFor(x => x.CompanyId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CommandRegisterUser, User>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _context = context;
            }

            public async Task<User> Handle(CommandRegisterUser request, CancellationToken cancellationToken)
            {
                //logic goes here
                if (await _context.Users.AnyAsync(x => x.Email == request.Email))
                {
                    throw new RestException(HttpStatusCode.Conflict, string.Format($"Email {request.Email} already exist."));
                }

                if (await _context.Users.AnyAsync(x => x.UserName == request.UserName))
                {
                    throw new RestException(HttpStatusCode.Conflict, string.Format($"UserName {request.UserName} already exist."));
                }

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.UserName,
                    DepartmentId = request.DepartmentId,
                    PositionId = request.PositionId,
                    CompanyId = request.CompanyId
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user),
                        UserName = user.UserName
                    };
                }
                else
                {
                    throw new Exception("Problem creating user");
                }
            }
        }
    }
}