using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Positions
{
    public class CreatePosition
    {
        public class CommandCreatePosition : IRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class CommandValidator : AbstractValidator<CommandCreatePosition>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.IsEnable).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CommandCreatePosition>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandCreatePosition request, CancellationToken cancellationToken)
            {
                //logic goes here
                var position = new Position
                {
                    Name = request.Name,
                    Description = request.Description,
                    DateCreated = request.DateCreated,
                    IsEnable = request.IsEnable
                };

                _context.Position.Add(position);

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