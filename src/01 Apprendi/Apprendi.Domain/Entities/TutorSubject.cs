namespace Apprendi.Domain.Entities
{
    public class TutorSubject
    {
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
