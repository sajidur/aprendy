using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TutorTeachingMaterialConfiguration : IEntityTypeConfiguration<TutorTeachingMaterial>
    {
        public void Configure(EntityTypeBuilder<TutorTeachingMaterial> builder)
        {
            builder.HasKey(teachingMaterial => new { teachingMaterial.TutorId, teachingMaterial.TeachingMaterialId });

            builder.HasOne<Tutor>()
                   .WithMany()
                   .HasForeignKey(teachingMaterial => teachingMaterial.TutorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<TeachingMaterial>()
                   .WithMany()
                   .HasForeignKey(teachingMaterial => teachingMaterial.TeachingMaterialId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

