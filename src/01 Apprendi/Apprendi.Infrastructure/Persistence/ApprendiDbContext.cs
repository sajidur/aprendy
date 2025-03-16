using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Common.Services;
using Apprendi.Domain.Common;
using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apprendi.Infrastructure.Persistence
{
    public class ApprendiDbContext : DbContext, IApprendiDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeOffset _dateTimeOffset;

        public ApprendiDbContext(ICurrentUserService currentUserService, IDateTimeOffset dateTimeOffset, DbContextOptions options) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeOffset = dateTimeOffset;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApprendiDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<LanguageProficiencyLevel> LanguageProficiencyLevels { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Domain.Entities.TimeZone> TimeZones { get; set; }
        public DbSet<TimeZoneData> TimeZoneData { get; set; }
        public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
        public DbSet<TeachingCertificate> TeachingCertificates { get; set; }
        public DbSet<TeachingPreference> TeachingPreferences { get; set; }
        public DbSet<TeachingMaterial> TeachingMaterials { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {            
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        if (entry.State == EntityState.Added)
                        {
                            entry.Entity.CreatedBy = _currentUserService?.User;
                            entry.Entity.Created = _dateTimeOffset.UtcNow;
                        }
                        entry.Entity.LastModifiedBy = _currentUserService?.User;
                        entry.Entity.LastModified = _dateTimeOffset.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
