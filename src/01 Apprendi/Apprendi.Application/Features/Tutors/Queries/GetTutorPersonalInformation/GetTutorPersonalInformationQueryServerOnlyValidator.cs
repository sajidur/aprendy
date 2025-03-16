using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.Tutors.Queries.GetTutorPersonalInformation;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Command.GetTutorPersonalInformation
{
    public class GetTutorPersonalInformationQueryServerOnlyValidator : ServerOnlyRequestValidator<GetTutorPersonalInformationQuery, GetTutorPersonalInformationQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorPersonalInformationQueryServerOnlyValidator(IResponseFactory<GetTutorPersonalInformationQueryResponse> responseFactory,
                                                                   IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorPersonalInformationQueryResponse> Handle(GetTutorPersonalInformationQuery request, CancellationToken cancellationToken)
        {
            var exists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.User.Email == request.Email, cancellationToken);

            if (!exists)
            {
                return Response.NotFound("Tutor not found");
            }

            return Response.Success();
        }
    }
}
