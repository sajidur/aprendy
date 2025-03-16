namespace Apprendi.Domain.Entities
{
    public class SpokenLanguage
    {
        public int TutorId { get; set; } 
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int ProficiencyLevelId { get; set; }
        public LanguageProficiencyLevel ProficiencyLevel { get; set; } 
    }
}
