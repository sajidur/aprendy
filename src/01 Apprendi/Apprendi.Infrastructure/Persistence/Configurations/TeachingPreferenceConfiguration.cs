using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TeachingPreferenceConfiguration : IEntityTypeConfiguration<TeachingPreference>
    {
        public void Configure(EntityTypeBuilder<TeachingPreference> builder)
        {
            builder.HasKey(teachingPreference => teachingPreference.Id);

            builder
                .Property(teachingPreference => teachingPreference.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(GetSeedData());
        }

        private static IEnumerable<TeachingPreference> GetSeedData()
        {
            yield return new TeachingPreference { Id = 1, Name = "Music" };
            yield return new TeachingPreference { Id = 2, Name = "Films and TV Series" };
            yield return new TeachingPreference { Id = 3, Name = "Art" };
            yield return new TeachingPreference { Id = 4, Name = "Business and Finance" };
            yield return new TeachingPreference { Id = 5, Name = "Pets and Animals" };
            yield return new TeachingPreference { Id = 6, Name = "Legal Services" };
            yield return new TeachingPreference { Id = 7, Name = "Environment and Nature" };
            yield return new TeachingPreference { Id = 8, Name = "Sports and Fitness" };
            yield return new TeachingPreference { Id = 9, Name = "Reading" };
            yield return new TeachingPreference { Id = 10, Name = "History" };
            yield return new TeachingPreference { Id = 11, Name = "Medical and Healthcare" };
            yield return new TeachingPreference { Id = 12, Name = "Gaming" };
            yield return new TeachingPreference { Id = 13, Name = "Marketing" };
            yield return new TeachingPreference { Id = 14, Name = "Animation and Comics" };
            yield return new TeachingPreference { Id = 15, Name = "Food" };
            yield return new TeachingPreference { Id = 16, Name = "Writing" };
            yield return new TeachingPreference { Id = 17, Name = "Science" };
            yield return new TeachingPreference { Id = 18, Name = "Technology" };
            yield return new TeachingPreference { Id = 19, Name = "Travel" };
            yield return new TeachingPreference { Id = 20, Name = "Fashion and Beauty" };
        }
    }
}

