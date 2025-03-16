namespace Apprendi.Application.Features.Tutors
{
    public class TutorTeachingProfileDto
    {
        public int TutorId { get; set; }
        public List<int> TeachingSubjectsIds { get; set; } = new();
        public int? TeachingExperienceInYears { get; set; }
        public List<int> TeachingCertificatesIds { get; set; } = new();
        public string OtherCertificates { get; set; }
        public bool IsOtherCertificateSpecified { get; set; }
        public string VideoIntroductionFileName { get; set; }
        public string AboutMe { get; set; }
        public List<int> TeachingMaterialsIds { get; set; } = new();
        public List<int> TeachingPreferencesIds { get; set; } = new();
    }

}
