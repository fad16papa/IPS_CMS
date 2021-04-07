using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Departments
{
    public class EditDepartment
    {
        public class CommandEditDepartment : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class Handler : IRequestHandler<CommandEditDepartment>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandEditDepartment request, CancellationToken cancellationToken)
            {
                //logic goes here
                var department = await _context.Department.FindAsync(request.Id);

                if (department == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                department.Name = request.Name ?? department.Name;
                department.Description = request.Description ?? department.Description;
                department.IsEnable = request.IsEnable;

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