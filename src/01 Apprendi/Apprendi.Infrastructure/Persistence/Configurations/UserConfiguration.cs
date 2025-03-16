using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Apprendi.Infrastructure.Persistence.Configurations
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .UseTptMappingStrategy();

            builder
                .HasKey(user => user.Id);

            builder
                .Property(user => user.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(user => user.LastName)
                .IsRequired(false)
                .HasMaxLength(100);

            builder
                .Property(user => user.Email)                
                .IsRequired()
                .HasMaxLength(254);

            builder
                .HasIndex(user => user.Email)
                .IsUnique();

            builder
                .Property(user => user.TimeZoneId)
                .IsRequired(false)
                .HasMaxLength(35);

            builder
                .Property(user => user.Phone)
                .IsRequired(false)
                .HasMaxLength(15);

            builder
                .Property(user => user.CurrencyId)
                .IsRequired(false);

            builder
                .HasOne(user => user.Currency)
                .WithMany()
                .HasForeignKey(user => user.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(user => user.LanguageId)
                .IsRequired(false);

            builder
                .HasOne(user => user.Language)
                .WithMany()
                .HasForeignKey(user => user.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(user => user.TimeZone)
                .WithMany()
                .HasForeignKey(user => user.TimeZoneId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

