using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetCountries
{
    public class GetCountriesQueryHandler : RequestHandler<GetCountriesQuery, GetCountriesResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetCountriesQueryHandler(IResponseFactory<GetCountriesResponse> responseFactory, 
                                       IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

       
        public override async Task<GetCountriesResponse> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {

            var countries = await _context.Countries.AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.Countries = countries;
            });
        }

        
    }
}
