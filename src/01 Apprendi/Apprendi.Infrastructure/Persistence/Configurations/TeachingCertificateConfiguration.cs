using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TeachingCertificateConfiguration : IEntityTypeConfiguration<TeachingCertificate>
    {
        public void Configure(EntityTypeBuilder<TeachingCertificate> builder)
        {
            builder.HasKey(teachingCertificate => teachingCertificate.Id);

            builder
                .Property(teachingCertificate => teachingCertificate.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(teachingCertificate => teachingCertificate.Description)
                .HasMaxLength(500);

            builder.HasData(GetSeedData());
        }

        private static IEnumerable<TeachingCertificate> GetSeedData()
        {
            yield return new TeachingCertificate
            {
                Id = 1,
                Name = "TESOL",
                Description = "Teaching English to Speakers of Other Languages - a certification for teaching English globally."
            };

            yield return new TeachingCertificate
            {
                Id = 2,
                Name = "TEFL",
                Description = "Teaching English as a Foreign Language - widely accepted qualification for teaching English abroad."
            };

            yield return new TeachingCertificate
            {
                Id = 3,
                Name = "CELTA",
                Description = "Certificate in English Language Teaching to Adults - awarded by Cambridge English."
            };

            yield return new TeachingCertificate
            {
                Id = 4,
                Name = "DELTA",
                Description = "Diploma in Teaching English to Speakers of Other Languages - an advanced qualification."
            };

            yield return new TeachingCertificate
            {
                Id = 5,
                Name = "IELTS Trainer",
                Description = "Certification for professionals training students for the International English Language Testing System."
            };

            yield return new TeachingCertificate
            {
                Id = 6,
                Name = "ACTFL",
                Description = "American Council on the Teaching of Foreign Languages - proficiency-based teaching certification."
            };
        }
    }
}

