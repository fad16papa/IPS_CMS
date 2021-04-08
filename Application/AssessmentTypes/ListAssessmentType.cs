using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AssessmentTypes
{
    public class ListAssessmentType
    {
        public class Query : IRequest<List<AssessmentType>>
        {

        }

        public class Handler : IRequestHandler<Query, List<AssessmentType>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<AssessmentType>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var listAssessmentType = await _context.AssessmentType.ToListAsync();

                return listAssessmentType;
            }
        }
    }
}