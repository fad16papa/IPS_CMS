using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Companies
{
    public class EditCompany
    {
        public class CommandEditCompany : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class Handler : IRequestHandler<CommandEditCompany>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandEditCompany request, CancellationToken cancellationToken)
            {
                //logic goes here
                var company = await _context.Company.FindAsync(request.Id);

                if (company == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not found");
                }

                company.Name = request.Name ?? company.Name;
                company.Description = request.Description ?? company.Description;
                company.IsEnable = request.IsEnable;

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