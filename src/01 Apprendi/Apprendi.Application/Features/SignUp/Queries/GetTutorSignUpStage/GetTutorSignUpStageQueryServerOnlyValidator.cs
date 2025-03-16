using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Queries;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Queries.GetTutorSignUpStage
{
    public class GetTutorSignUpStageQueryServerOnlyValidator : ServerOnlyRequestValidator<GetTutorSignUpStageQuery, GetTutorSignUpStageQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorSignUpStageQueryServerOnlyValidator(IResponseFactory<GetTutorSignUpStageQueryResponse> responseFactory,
                                                    IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorSignUpStageQueryResponse> Handle(GetTutorSignUpStageQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.Id == request.TutorId, cancellationToken);

            if (!userExists)
            {
                return Response.NotFound("Tutor not found");
            }

            return Response.Success();
        }
    }
}
