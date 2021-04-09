using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.AssessmentQuestions
{
    public class EditAssessmentQuestion
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Question { get; set; }
            public string Name { get; set; }
            public Guid AssessmentTypeId { get; set; }
            public string Points { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //logic goes here
                var assessmentQueston = await _context.AssessmentQuestion.FindAsync(request.Id);

                if (assessmentQueston == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                assessmentQueston.Name = request.Name ?? assessmentQueston.Name;
                assessmentQueston.Question = request.Question ?? assessmentQueston.Question;
                assessmentQueston.Points = Convert.ToInt32(request.Points);
                assessmentQueston.IsEnable = request.IsEnable;

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