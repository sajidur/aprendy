using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(student => student.Id);

            builder.HasOne(student => student.User)
                   .WithOne()
                   .HasForeignKey<Student>(student => student.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

