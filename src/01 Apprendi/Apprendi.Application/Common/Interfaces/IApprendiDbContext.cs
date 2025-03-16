using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apprendi.Application.Common.Interfaces
{
    public interface IApprendiDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Student> Students { get; }
        DbSet<Tutor> Tutors { get; }
        DbSet<Subject> Subjects { get; }        
        DbSet<LanguageProficiencyLevel> LanguageProficiencyLevels { get; }
        DbSet<SpokenLanguage> SpokenLanguages { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Language> Languages { get; }
        DbSet<TeachingCertificate> TeachingCertificates { get; }
        DbSet<Currency> Currencies { get; }
        DbSet<Country> Countries { get; }
        DbSet<TeachingPreference> TeachingPreferences { get; }
        DbSet<TeachingMaterial> TeachingMaterials { get; }
        DbSet<Domain.Entities.TimeZone> TimeZones { get; }
        DbSet<TimeZoneData> TimeZoneData { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
