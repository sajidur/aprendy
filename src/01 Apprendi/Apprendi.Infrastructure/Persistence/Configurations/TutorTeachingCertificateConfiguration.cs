using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TutorTeachingCertificateConfiguration : IEntityTypeConfiguration<TutorTeachingCertificate>
    {
        public void Configure(EntityTypeBuilder<TutorTeachingCertificate> builder)
        {
            builder.HasKey(teachingCertificate => new { teachingCertificate.TutorId, teachingCertificate.TeachingCertificateId });

            builder.HasOne<Tutor>()
                   .WithMany()
                   .HasForeignKey(teachingCertificate => teachingCertificate.TutorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<TeachingCertificate>()
                   .WithMany()
                   .HasForeignKey(teachingCertificate => teachingCertificate.TeachingCertificateId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

