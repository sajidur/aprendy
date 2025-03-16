using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetSpokenLanguages
{
    public class GetSpokenLanguagesQueryHandler : RequestHandler<GetSpokenLanguagesQuery, GetSpokenLanguagesQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetSpokenLanguagesQueryHandler(IResponseFactory<GetSpokenLanguagesQueryResponse> responseFactory, 
                                                   IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

       
        public override async Task<GetSpokenLanguagesQueryResponse> Handle(GetSpokenLanguagesQuery request, CancellationToken cancellationToken)
        {

            var spokenLanguages = await _context
                .SpokenLanguages
                .AsNoTracking()
                .ProjectToDto()
                .ToListAsync(cancellationToken);

            return Response.Success(response =>
            {
                response.SpokenLanguages = spokenLanguages;
            });
        }
    }
}
