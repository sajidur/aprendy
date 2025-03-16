using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder
                .HasKey(language => language.Id);

            builder
                .Property(language => language.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .Property(language => language.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasData(GetSeedData());
        }

        private static IEnumerable<Language> GetSeedData()
        {
            yield return new Language { Id = 1, Code = "EN", Name = "English" }; 
            yield return new Language { Id = 2, Code = "FR", Name = "French" };
            yield return new Language { Id = 3, Code = "ES", Name = "Spanish" };
            yield return new Language { Id = 4, Code = "DE", Name = "German" };
            yield return new Language { Id = 5, Code = "ZH", Name = "Chinese" };
        }
    }
}

