using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile
{
    public class GetTutorTeachingProfileQueryServerOnlyValidator : ServerOnlyRequestValidator<GetTutorTeachingProfileQuery, GetTutorTeachingProfileQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTutorTeachingProfileQueryServerOnlyValidator(IResponseFactory<GetTutorTeachingProfileQueryResponse> responseFactory,
                                                          IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetTutorTeachingProfileQueryResponse> Handle(GetTutorTeachingProfileQuery request, CancellationToken cancellationToken)
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
