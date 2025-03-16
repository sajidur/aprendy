using Apprendi.Domain.Entities;
using Apprendi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder.HasKey(tutor => tutor.Id);

            builder.HasOne(tutor => tutor.User)
                   .WithOne()
                   .HasForeignKey<Tutor>(tutor => tutor.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(tutor => tutor.SignUpStage)
                .IsRequired()
                .HasDefaultValue(TutorSignUpStage.PersonalInformation)
                .HasConversion(v => $"{v}",v => Enum.Parse<TutorSignUpStage>(v));

            builder
                .HasOne(tutor => tutor.CountryOfBirth)
                .WithMany()
                .HasForeignKey(tutor => tutor.CountryOfBirthId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tutor => tutor.CountryResidency)
                .WithMany()
                .HasForeignKey(tutor => tutor.CountryResidencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(tutor => tutor.Nickname)
                .HasMaxLength(50)
                .IsRequired(false); 

            builder
                .Property(tutor => tutor.IsOver18)
                .IsRequired();

            builder
                .Property(tutor => tutor.DateOfBirth)
                .IsRequired();

            builder.Property(tutor => tutor.PhotoProfileFileName)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(tutor => tutor.VideoIntroductionFileName)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(tutor => tutor.AboutMe)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(tutor => tutor.TeachingExperienceInYears)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(tutor => tutor.OtherCertificates)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(tutor => tutor.IsPhotoPolicyAgreed)
                .IsRequired();

            builder.Property(tutor => tutor.IsOtherCertificateSpecified)
                .IsRequired();

            builder
                .HasMany(tutor => tutor.SpokenLanguages)
                .WithOne()
                .HasForeignKey(languageDetail => languageDetail.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tutor => tutor.TeachingSubjects)
                .WithOne()
                .HasForeignKey(tutorSubject => tutorSubject.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tutor => tutor.TeachingCertificates)
                .WithOne()
                .HasForeignKey(teachingCertificate => teachingCertificate.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tutor => tutor.TeachingMaterials)
                .WithOne()
                .HasForeignKey(teachingMaterial => teachingMaterial.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tutor => tutor.TeachingPreferences)
                .WithOne()
                .HasForeignKey(teachingPreference => teachingPreference.TutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

