using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Queries.GetTutorSignUpStage
{
    public class GetTutorAboutStageDetailsQueryServerOnlyValidator : ServerOnlyRequestValidator<GetTutorAboutStageDetailsQuery, GetTutorAboutStageDetailsQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorAboutStageDetailsQueryServerOnlyValidator(IResponseFactory<GetTutorAboutStageDetailsQueryResponse> responseFactory,
                                                                 IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorAboutStageDetailsQueryResponse> Handle(GetTutorAboutStageDetailsQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.User.Email == request.Email, cancellationToken);

            if (!userExists)
            {
                return Response.NotFound("Tutor not found");
            }

            return Response.Success();
        }
    }
}
