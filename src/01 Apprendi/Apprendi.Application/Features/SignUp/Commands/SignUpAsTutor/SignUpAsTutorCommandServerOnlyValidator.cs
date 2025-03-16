using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor;
using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.SignUp.Commands.RegisterUserAsTutor
{
    public class SignUpAsTutorCommandServerOnlyValidator : ServerOnlyRequestValidator<SignUpAsTutorCommand, SignUpAsTutorCommandResponse>
    {
        private readonly IApprendiDbContext _context;

        public SignUpAsTutorCommandServerOnlyValidator(IResponseFactory<SignUpAsTutorCommandResponse> responseFactory,
                                                               IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        public override async Task<SignUpAsTutorCommandResponse> Handle(SignUpAsTutorCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context
                .Tutors
                .AnyAsync(tutor => tutor.User.Email == request.Email, cancellationToken);

            if (exists) return Response.ConflictFailure("Tutor already exists");

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
