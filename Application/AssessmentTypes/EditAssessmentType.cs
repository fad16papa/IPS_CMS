using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.AssessmentTypes
{
    public class EditAssessmentType
    {
        public class CommandEditAssessmentType : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class Handler : IRequestHandler<CommandEditAssessmentType>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandEditAssessmentType request, CancellationToken cancellationToken)
            {
                //logic goes here
                var assessmentType = await _context.AssessmentType.FindAsync(request.Id);

                if (assessmentType == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                assessmentType.Name = request.Name ?? assessmentType.Name;
                assessmentType.Description = request.Description ?? assessmentType.Description;
                assessmentType.IsEnable = request.IsEnable;

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