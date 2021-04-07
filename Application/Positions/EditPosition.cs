using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Positions
{
    public class EditPosition
    {
        public class CommandEditPosition : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class Handler : IRequestHandler<CommandEditPosition>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandEditPosition request, CancellationToken cancellationToken)
            {
                //logic goes here
                var position = await _context.Position.FindAsync(request.Id);

                if (position == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                position.Name = request.Name ?? position.Name;
                position.Description = request.Description ?? position.Description;
                position.IsEnable = request.IsEnable;

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