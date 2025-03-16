using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(role => role.Id);

            builder
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .HasData(GetSeedData());
        }

        private static IEnumerable<Role> GetSeedData()
        {
            yield return new Role { Id = 1, Name = "Admin" };
            yield return new Role { Id = Role.TutorRoleId, Name = "Tutor" };
            yield return new Role { Id = Role.StudentRoleId, Name = "Student" };
        }
    }
}

