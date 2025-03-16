using Apprendi.Domain.Enums;

namespace Apprendi.Domain.Entities
{
    public class Tutor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TutorSignUpStage SignUpStage { get; set; }
        public string CountryOfBirthId { get; set; }
        public Country CountryOfBirth { get; set; }
        public string CountryResidencyId { get; set; }
        public Country CountryResidency { get; set; }
        public string Nickname { get; set; }
        public DateTime DateOfBirth { get;set; }        
        public virtual List<SpokenLanguage> SpokenLanguages { get; set; } = new();
        public string PhotoProfileFileName { get; set; }
        public bool IsPhotoPolicyAgreed { get; set; }
        public bool IsOver18 { get; set; }
        public virtual List<TutorSubject> TeachingSubjects { get; set; } = new();
        public int TeachingExperienceInYears { get; set; }
        public virtual List<TutorTeachingCertificate> TeachingCertificates { get; set; } = new();
        public string OtherCertificates { get; set; }
        public bool IsOtherCertificateSpecified { get; set; }
        public string VideoIntroductionFileName { get; set; }
        public string AboutMe { get; set; }
        public virtual List<TutorTeachingMaterial> TeachingMaterials { get; set; } = new();
        public virtual List<TutorTeachingPreference> TeachingPreferences { get; set; } = new();
    }
}
