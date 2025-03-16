using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TutorSubjectConfiguration : IEntityTypeConfiguration<TutorSubject>
    {
        public void Configure(EntityTypeBuilder<TutorSubject> builder)
        {
            builder.HasKey(teachingSubject => new { teachingSubject.TutorId, teachingSubject.SubjectId });

            builder.HasOne<Tutor>()
                   .WithMany()
                   .HasForeignKey(teachingSubject => teachingSubject.TutorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Subject>()
                   .WithMany()
                   .HasForeignKey(teachingSubject => teachingSubject.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

