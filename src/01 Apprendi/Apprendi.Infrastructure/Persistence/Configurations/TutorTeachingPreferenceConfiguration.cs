using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TutorTeachingPreferenceConfiguration : IEntityTypeConfiguration<TutorTeachingPreference>
    {
        public void Configure(EntityTypeBuilder<TutorTeachingPreference> builder)
        {
            builder.HasKey(teachingPreference => new { teachingPreference.TutorId, teachingPreference.TeachingPreferenceId });

            builder.HasOne<Tutor>()
                   .WithMany()
                   .HasForeignKey(teachingPreference => teachingPreference.TutorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<TeachingPreference>()
                   .WithMany()
                   .HasForeignKey(teachingPreference => teachingPreference.TeachingPreferenceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

