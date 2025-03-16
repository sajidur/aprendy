using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetReferenceData
{
    public class GetReferenceDataQueryHandler : RequestHandler<GetReferenceDataQuery, GetReferenceDataQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetReferenceDataQueryHandler(IResponseFactory<GetReferenceDataQueryResponse> responseFactory, 
                                     IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetReferenceDataQueryResponse> Handle(GetReferenceDataQuery request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var languages = await _context.Languages.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            var currencies = await _context.Currencies.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);
            
            return Response.Success(response =>
            {
                response.Roles = roles;
                response.Languages = languages;
                response.Currencies = currencies;
            });
        }
    }
}
