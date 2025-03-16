using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Queries.GetTutorSignUpStage
{
    public class GetTutorSignUpStageQueryHandler : RequestHandler<GetTutorSignUpStageQuery, GetTutorSignUpStageQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorSignUpStageQueryHandler(IResponseFactory<GetTutorSignUpStageQueryResponse> responseFactory,
                                          IApprendiDbContext context)
            : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorSignUpStageQueryResponse> Handle(GetTutorSignUpStageQuery request, CancellationToken cancellationToken)
        {
            var signUpStage = await _context
                .Tutors
                .Where(tutor => tutor.Id == request.TutorId)
                .AsNoTracking()                
                .Select(tutor => tutor.SignUpStage)
                .FirstAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.SignUpStage = signUpStage;
            });
        }
    }
}
