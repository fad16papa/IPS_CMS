using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Companies
{
    public class CreateCompany
    {
        public class CommandCreateCompany : IRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class CommandValidator : AbstractValidator<CommandCreateCompany>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.IsEnable).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CommandCreateCompany>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandCreateCompany request, CancellationToken cancellationToken)
            {
                //logic goes here
                var company = new Company()
                {
                    Name = request.Name,
                    Description = request.Description,
                    DateCreated = request.DateCreated,
                    IsEnable = request.IsEnable
                };

                _context.Company.Add(company);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
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