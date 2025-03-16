using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class LanguageDetailConfiguration : IEntityTypeConfiguration<SpokenLanguage>
    {
        public void Configure(EntityTypeBuilder<SpokenLanguage> builder)
        {
            builder
                .HasKey(languageDetail => new { languageDetail.TutorId, languageDetail.LanguageId });

            builder
                .HasOne(languageDetail => languageDetail.Language)
                .WithMany()
                .HasForeignKey(languageDetail => languageDetail.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(languageDetail => languageDetail.ProficiencyLevel)
                .WithMany()
                .HasForeignKey(languageDetail => languageDetail.ProficiencyLevelId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .HasOne<Tutor>()
                .WithMany(tutor => tutor.SpokenLanguages)
                .HasForeignKey(languageDetail => languageDetail.TutorId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

