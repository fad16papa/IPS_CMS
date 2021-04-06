using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
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
        public class Command : IRequest<User>
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string DisplayName { get; set; }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext _context;

            public Handler(DataContext context, UserManager<AppUser> userManager)
            {
                _context = context;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                //logic goes here
                if(await _context.Users.AnyAsync(x => x.Email == request.Email))
                {
                    throw new RestException(HttpStatusCode.Conflict, string.Format($"Email {request.Email} already exist."));
                }
                
                if(await _context.Users.AnyAsync(x => x.UserName == request.UserName))
                {
                    throw new RestException(HttpStatusCode.Conflict, string.Format($"Email {request.UserName} already exist."));
                }

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.UserName,
                };

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return new User() { };
                }
                else
                {
                    throw new Exception("Problem saving changes");
                }
            }
        }
    }
}