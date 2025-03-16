using Apprendi.Application.Features.ReferenceData;

namespace Apprendi.Application.Features.Tutors
{
    public class TutorPersonalInformationDto
    {
        public int TutorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryOfBirthId { get; set; }
        public string CountryResidencyId { get; set; }
        public string Nickname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<SpokenLanguageDto> SpokenLanguages { get; set; } = new();
        public string PhotoProfileFileName { get; set; }
        public bool IsPhotoPolicyAgreed { get; set; }
    }

}
