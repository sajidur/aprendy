using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Apprendi.Infrastructure.Persistence
{
    public class ApprendiDbContextFactory : IDesignTimeDbContextFactory<ApprendiDbContext>
    {
        public ApprendiDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=.;Database=ApprendiDbContext;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<ApprendiDbContext>();            
            optionsBuilder.UseSqlServer(connectionString);

            var dateTimeOffset = new DateTimeOffsetWrapper();

            return new ApprendiDbContext(null, dateTimeOffset, optionsBuilder.Options);
        }
    }
}
