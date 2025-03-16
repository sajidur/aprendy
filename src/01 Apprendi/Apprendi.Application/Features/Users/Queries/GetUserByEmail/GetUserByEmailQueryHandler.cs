using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : RequestHandler<GetUserByEmailQuery, GetUserByEmailQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetUserByEmailQueryHandler(IResponseFactory<GetUserByEmailQueryResponse> responseFactory, 
                                          IApprendiDbContext context) 
            : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<GetUserByEmailQueryResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            TutorInformationDto tutorInformationDto = null;

            var userDto = await _context
                .Users
                .Where(x => x.Email == request.Email)
                .AsNoTracking()
                .ProjectToDto()
                .SingleAsync(cancellationToken);

            if (userDto.RoleIds.Contains(Role.TutorRoleId))
            {
                tutorInformationDto = await _context
                    .Tutors
                    .Where(tutor => tutor.UserId == userDto.Id)
                    .AsNoTracking()
                    .ProjectToTutorInformationDto()
                    .SingleAsync(cancellationToken);
                
            }

            return Response.Success(response =>
            {
                response.User = userDto;
                response.TutorInformation = tutorInformationDto;
            });
        }
    }
}
