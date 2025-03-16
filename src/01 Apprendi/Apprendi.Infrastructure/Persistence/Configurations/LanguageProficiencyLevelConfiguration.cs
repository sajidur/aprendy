using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class LanguageProficiencyLevelConfiguration : IEntityTypeConfiguration<LanguageProficiencyLevel>
    {
        public void Configure(EntityTypeBuilder<LanguageProficiencyLevel> builder)
        {
            builder
                .HasKey(level => level.Id);

            builder
                .Property(level => level.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasData(GetSeedData());
        }

        private static IEnumerable<LanguageProficiencyLevel> GetSeedData()
        {
            yield return new LanguageProficiencyLevel { Id = 1, Name = "A1" };
            yield return new LanguageProficiencyLevel { Id = 2, Name = "A2" };
            yield return new LanguageProficiencyLevel { Id = 3, Name = "B1" };
            yield return new LanguageProficiencyLevel { Id = 4, Name = "B2" };
            yield return new LanguageProficiencyLevel { Id = 5, Name = "C1" };
            yield return new LanguageProficiencyLevel { Id = 6, Name = "C2" };
            yield return new LanguageProficiencyLevel { Id = 7, Name = "Native" };
        }
    }
}

