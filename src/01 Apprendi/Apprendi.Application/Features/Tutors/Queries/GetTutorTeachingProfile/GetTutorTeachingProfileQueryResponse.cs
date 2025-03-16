using Apprendi.Application.Common.Base;
using Apprendi.Application.Features.ReferenceData;

namespace Apprendi.Application.Features.Tutors.Queries.GetTutorTeachingProfile
{
    public class GetTutorTeachingProfileQueryResponse : Response
    {
        public TutorTeachingProfileDto TeachingProfile { get; set; } = new();
        public List<SubjectDto> TeachingSubjects { get; set; } = new();
        public List<TeachingCertificateDto> TeachingCertificates { get; set; } = new();
        public List<TeachingMaterialDto> TeachingMaterials { get; set; } = new();
        public List<TeachingPreferenceDto> TeachingPreferences { get; set; } = new();
    }
}
