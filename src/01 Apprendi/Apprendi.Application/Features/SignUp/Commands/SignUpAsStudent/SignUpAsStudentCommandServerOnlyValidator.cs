using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent;
using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Commands.RegisterUserAsStudent
{
    public class SignUpAsStudentCommandServerOnlyValidator : ServerOnlyRequestValidator<SignUpAsStudentCommand, SignUpAsStudentCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public SignUpAsStudentCommandServerOnlyValidator(IResponseFactory<SignUpAsStudentCommandResponse> responseFactory,
                                                               IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<SignUpAsStudentCommandResponse> Handle(SignUpAsStudentCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context
                .Users
                .AnyAsync(user => user.Email == request.Email, cancellationToken);

            if (exists) return Response.ConflictFailure("User already exists");

            exists = await _context
                .Languages
                .AnyAsync(language => language.Id == request.LanguageId, cancellationToken);

            if (!exists) return Response.NotFound("Invalid Language");

            exists = await _context
                .Currencies
                .AnyAsync(currency => currency.Id == request.CurrencyId, cancellationToken);

            if (!exists) return Response.NotFound("Invalid Currency");

            exists = await _context
                .TimeZones
                .AnyAsync(timeZone => timeZone.Id == request.TimeZoneId, cancellationToken);

            if (!exists) return Response.NotFound("Invalid TimeZone");

            return Response.Success();
        }
    }
}
