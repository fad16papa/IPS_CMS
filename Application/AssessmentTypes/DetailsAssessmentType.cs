using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.AssessmentTypes
{
    public class DetailsAssessmentType
    {
        public class Query : IRequest<AssessmentType>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, AssessmentType>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<AssessmentType> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var assessmentType = await _context.AssessmentType.FindAsync(request.Id);

                if (assessmentType == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not Found");
                };

                return assessmentType;
            }
        }
    }
}