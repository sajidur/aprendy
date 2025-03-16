using Apprendi.Domain.Entities;

namespace Apprendi.Application.Features.ReferenceData
{
    public static class ReferenceDataMapper
    {
        public static IQueryable<RoleDto> ProjectToDto(this IQueryable<Role> roles)
        {
            return roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        public static IQueryable<CountryDto> ProjectToDto(this IQueryable<Country> country)
        {
            return country.Select(country => new CountryDto
            {
                Id = country.Id,
                Name = country.CountryName
            });
        }

        public static IQueryable<LanguageProficiencyLevelDto> ProjectToDto(this IQueryable<LanguageProficiencyLevel> languageProficiencyLevel)
        {
            return languageProficiencyLevel.Select(languageProficiencyLevel => new LanguageProficiencyLevelDto
            {
                Id = languageProficiencyLevel.Id,
                Name = languageProficiencyLevel.Name
            });
        }

        public static IQueryable<SpokenLanguageDto> ProjectToDto(this IQueryable<SpokenLanguage> spokenLanguage)
        {
            return spokenLanguage.Select(spokenLanguage => new SpokenLanguageDto
            {
                LanguageId = spokenLanguage.LanguageId,
                ProficiencyLevelId = spokenLanguage.ProficiencyLevelId
            });
        }

        public static IQueryable<LanguageDto> ProjectToDto(this IQueryable<Language> roles)
        {
            return roles.Select(role => new LanguageDto
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        public static IQueryable<TeachingCertificateDto> ProjectToDto(this IQueryable<TeachingCertificate> teachingCertificate)
        {
            return teachingCertificate.Select(role => new TeachingCertificateDto
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        public static IQueryable<CurrencyDto> ProjectToDto(this IQueryable<Currency> currencies)
        {
            return currencies.Select(currency => new CurrencyDto
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Symbol = currency.Symbol,
                DecimalPlaces = currency.DecimalPlaces
            });
        }

        public static IQueryable<SubjectDto> ProjectToDto(this IQueryable<Subject> subject)
        {
            return subject.Select(subject => new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name
            });
        }

        public static IQueryable<TeachingPreferenceDto> ProjectToDto(this IQueryable<TeachingPreference> teachingPreference)
        {
            return teachingPreference.Select(subject => new TeachingPreferenceDto
            {
                Id = subject.Id,
                Name = subject.Name
            });
        }

        public static IQueryable<TeachingMaterialDto> ProjectToDto(this IQueryable<TeachingMaterial> teachingMaterial)
        {
            return teachingMaterial.Select(subject => new TeachingMaterialDto
            {
                Id = subject.Id,
                Name = subject.Name
            });
        }
    }
}
