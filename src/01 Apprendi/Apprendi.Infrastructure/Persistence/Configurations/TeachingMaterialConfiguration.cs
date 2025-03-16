using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TeachingMaterialConfiguration : IEntityTypeConfiguration<TeachingMaterial>
    {
        public void Configure(EntityTypeBuilder<TeachingMaterial> builder)
        {
            builder.HasKey(teachingMaterial => teachingMaterial.Id);

            builder
                .Property(teachingMaterial => teachingMaterial.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(GetSeedData());
        }

        private static IEnumerable<TeachingMaterial> GetSeedData()
        {
            yield return new TeachingMaterial { Id = 1, Name = "PDF File" };
            yield return new TeachingMaterial { Id = 2, Name = "Audio Files" };
            yield return new TeachingMaterial { Id = 3, Name = "Flashcards" };
            yield return new TeachingMaterial { Id = 4, Name = "Test Templates and Examples" };
            yield return new TeachingMaterial { Id = 5, Name = "Text Documents" };
            yield return new TeachingMaterial { Id = 6, Name = "Image Files" };
            yield return new TeachingMaterial { Id = 7, Name = "Articles and News" };
            yield return new TeachingMaterial { Id = 8, Name = "Graphs and Charts" };
            yield return new TeachingMaterial { Id = 9, Name = "Presentation Slides / PowerPoint" };
            yield return new TeachingMaterial { Id = 10, Name = "Video Files" };
            yield return new TeachingMaterial { Id = 11, Name = "Quizzes" };
            yield return new TeachingMaterial { Id = 12, Name = "Homework Assignments" };
        }
    }
}

