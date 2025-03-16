
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Features.ReferenceData;
using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apprendi.Application.Features.Tutors
{
    public interface ITutorMapper
    {
        void DtoToEntity(TutorPersonalInformationDto personalInformation, Tutor tutor);
        void DtoToEntity(TutorTeachingProfileDto teachingProfile, Tutor tutor);
    }

    public class TutorMapper : ITutorMapper
    {
        public void DtoToEntity(TutorPersonalInformationDto personalInformation, Tutor tutor)
        {
            tutor.User.FirstName = personalInformation.FirstName;
            tutor.User.LastName = personalInformation.LastName;

            tutor.CountryOfBirthId = personalInformation.CountryOfBirthId;
            tutor.CountryResidencyId = personalInformation.CountryResidencyId;

            tutor.Nickname = personalInformation.Nickname;

            tutor.DateOfBirth = personalInformation.DateOfBirth;
            
            tutor.IsPhotoPolicyAgreed = personalInformation.IsPhotoPolicyAgreed;            
            tutor.PhotoProfileFileName = personalInformation.PhotoProfileFileName;

            tutor.SpokenLanguages.Clear();

            var spokenLanguages = personalInformation.SpokenLanguages;
            tutor.SpokenLanguages.AddRange(spokenLanguages.Select(spokenLanguage => new SpokenLanguage
            {
                LanguageId = spokenLanguage.LanguageId,
                ProficiencyLevelId = spokenLanguage.ProficiencyLevelId
            }));
        }

        public void DtoToEntity(TutorTeachingProfileDto teachingProfile, Tutor tutor)
        {
            tutor.TeachingExperienceInYears = teachingProfile.TeachingExperienceInYears.Value;
            tutor.IsOtherCertificateSpecified = teachingProfile.IsOtherCertificateSpecified;
            tutor.VideoIntroductionFileName = teachingProfile.VideoIntroductionFileName;
            tutor.AboutMe = teachingProfile.AboutMe;

            tutor.TeachingSubjects = teachingProfile.TeachingSubjectsIds.Select(subjectId => new TutorSubject
            {
                SubjectId = subjectId,
                TutorId = tutor.Id
            }).ToList();

            tutor.TeachingCertificates = teachingProfile.TeachingCertificatesIds.Select(teachingCertificateId => new TutorTeachingCertificate
            {
                TeachingCertificateId = teachingCertificateId,
                TutorId = tutor.Id
            }).ToList();

            tutor.TeachingMaterials = teachingProfile.TeachingMaterialsIds.Select(teachingMaterialId => new TutorTeachingMaterial
            {
                TeachingMaterialId = teachingMaterialId,
                TutorId = tutor.Id
            }).ToList();

            tutor.TeachingPreferences = teachingProfile.TeachingPreferencesIds.Select(teachingPreferenceId => new TutorTeachingPreference
            {
                TeachingPreferenceId = teachingPreferenceId,
                TutorId = tutor.Id
            }).ToList();
        }
    }

    public static class TutorMapperProjector
    {
        public static IQueryable<TutorPersonalInformationDto> ProjectToTutorPersonalInformationDto(this IQueryable<Tutor> tutors)
        {
            return tutors.Select(tutor => new TutorPersonalInformationDto
            {
                TutorId = tutor.Id,
                FirstName = tutor.User.FirstName,
                LastName = tutor.User.LastName,
                CountryOfBirthId = tutor.CountryOfBirthId,
                CountryResidencyId = tutor.CountryResidencyId,
                DateOfBirth = tutor.DateOfBirth,
                IsPhotoPolicyAgreed = tutor.IsPhotoPolicyAgreed,
                Nickname = tutor.Nickname,
                PhotoProfileFileName = tutor.PhotoProfileFileName,
                SpokenLanguages = tutor.SpokenLanguages.Select(spokenLanguage => new SpokenLanguageDto
                {
                    LanguageId = spokenLanguage.LanguageId,
                    ProficiencyLevelId = spokenLanguage.ProficiencyLevelId
                }).ToList(),
            });
        }

        public static IQueryable<TutorTeachingProfileDto> ProjectToTutorTeachingProfileDto(this IQueryable<Tutor> tutors)
        {
            return tutors.Select(tutor => new TutorTeachingProfileDto
            {
                TutorId = tutor.Id,
                AboutMe = tutor.AboutMe,
                IsOtherCertificateSpecified = tutor.IsOtherCertificateSpecified,
                OtherCertificates = tutor.OtherCertificates,
                TeachingExperienceInYears = tutor.TeachingExperienceInYears,
                VideoIntroductionFileName = tutor.VideoIntroductionFileName,

                TeachingCertificatesIds = tutor
                    .TeachingCertificates
                    .Select(teachingCertificate => teachingCertificate.TeachingCertificateId).ToList(),
                
                TeachingMaterialsIds = tutor.TeachingMaterials
                    .Select(teachingMaterial => teachingMaterial.TeachingMaterialId)
                    .ToList(),
                
                TeachingPreferencesIds = tutor
                    .TeachingPreferences
                    .Select(teachingPreference => teachingPreference.TeachingPreferenceId)
                    .ToList(),
                
                TeachingSubjectsIds = tutor.TeachingSubjects
                    .Select(teachingSubject => teachingSubject.SubjectId)
                    .ToList(),
            });
        }
    }
}
