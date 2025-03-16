using Apprendi.Application.Common.Base;

namespace Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent
{
    public class SignUpAsStudentCommand : Request<SignUpAsStudentCommandResponse>
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int LanguageId { get; set; }
        public int CurrencyId { get; set; }
        public string TimeZoneId { get; set; }
    }
}

