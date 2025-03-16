using Apprendi.Domain.Entities;

namespace Apprendi.Application.Features.SignUp
{
    public static class SignUpMapper
    {
        public static IQueryable<SubjectDto> ProjectToDto(this IQueryable<Subject> country)
        {
            return country.Select(subject => new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name
            });
        }

        public static IQueryable<LanguageProficiencyLevelDto> ProjectToDto(this IQueryable<LanguageProficiencyLevel> country)
        {
            return country.Select(languageProficiencyLevel => new LanguageProficiencyLevelDto
            {
                Id = languageProficiencyLevel.Id,
                Name = languageProficiencyLevel.Name
            });
        }
    }
}
