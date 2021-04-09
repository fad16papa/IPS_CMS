using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AssessmentQuestions
{
    public class ListAssessmentQuestion
    {
        public class Query : IRequest<List<AssessmentQuestion>>
        {

        }

        public class Handler : IRequestHandler<Query, List<AssessmentQuestion>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<AssessmentQuestion>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var listAssessmentQuestions = await _context.AssessmentQuestion.ToListAsync();

                return listAssessmentQuestions;
            }
        }
    }
}