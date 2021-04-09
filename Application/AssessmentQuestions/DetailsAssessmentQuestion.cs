using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.AssessmentQuestions
{
    public class DetailsAssessmentQuestion
    {
        public class Query : IRequest<AssessmentQuestion>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, AssessmentQuestion>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<AssessmentQuestion> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var assessmentQueston = await _context.AssessmentQuestion.FindAsync(request.Id);

                if (assessmentQueston == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                }

                return assessmentQueston;
            }
        }
    }
}