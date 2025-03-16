using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetLanguageProficiencyLevels
{
    public class GetLanguageProficiencyLevelsQueryHandler : RequestHandler<GetLanguageProficiencyLevelsQuery, GetLanguageProficiencyLevelsQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetLanguageProficiencyLevelsQueryHandler(IResponseFactory<GetLanguageProficiencyLevelsQueryResponse> responseFactory, 
                                                   IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

       
        public override async Task<GetLanguageProficiencyLevelsQueryResponse> Handle(GetLanguageProficiencyLevelsQuery request, CancellationToken cancellationToken)
        {

            var languageProficiencyLevels = await _context
                .LanguageProficiencyLevels
                .AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.LanguageProficiencyLevels = languageProficiencyLevels;
            });
        }
    }
}
