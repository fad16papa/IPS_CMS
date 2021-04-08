using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.AssessmentQuestions
{
    public class CreateAssessmentQuestion
    {
        public class CommandCreateAssessmentQuestion : IRequest
        {
            public string Question { get; set; }
            public string Name { get; set; }
            public Guid AssessmentTypeId { get; set; }
            public int Points { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsEnable { get; set; }
        }

        public class CommandValidator : AbstractValidator<CommandCreateAssessmentQuestion>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Question).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.AssessmentTypeId).NotEmpty();
                RuleFor(x => x.Points).NotEmpty();
                RuleFor(x => x.IsEnable).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CommandCreateAssessmentQuestion>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CommandCreateAssessmentQuestion request, CancellationToken cancellationToken)
            {
                //logic goes here
                var assessmentQuestion = new AssessmentQuestion
                {
                    Name = request.Name,
                    Question = request.Question,
                    AssessmentTypeId = request.AssessmentTypeId,
                    Points = request.Points,
                    DateCreated = request.DateCreated,
                    IsEnable = request.IsEnable
                };

                _context.AssessmentQuestion.Add(assessmentQuestion);

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