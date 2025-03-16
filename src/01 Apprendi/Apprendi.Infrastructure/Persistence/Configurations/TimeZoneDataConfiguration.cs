using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{

    public class TimeZoneDataConfiguration : IEntityTypeConfiguration<TimeZoneData>
    {
        public void Configure(EntityTypeBuilder<TimeZoneData> builder)
        {
            builder
                .HasNoKey();

            builder
                .Ignore(timeZone => timeZone.TimeZoneId);

            builder
                .Property(timeZone => timeZone.ZoneName)
                .HasMaxLength(35)                
                .IsRequired();

            builder
                .HasIndex(timeZone => timeZone.ZoneName);

            builder                
                .Property(timeZone => timeZone.CountryCode)
                .HasMaxLength(2)
                .IsRequired();

            builder
                .Property(timeZone => timeZone.Abbreviation)
                .HasMaxLength(6)
                .IsRequired();

            builder
                .Property(timeZone => timeZone.TimeStart)
                .IsRequired();

            builder
                .HasIndex(timeZone => timeZone.TimeStart);

            builder
                .Property(timeZone => timeZone.GmtOffset)
                .IsRequired();

            builder
                .Property(timeZone => timeZone.DaylightSavingTime)
                .HasMaxLength(1)
                .IsRequired();

            builder.
                HasData(GetSeedData());
        }

        public static IEnumerable<TimeZoneData> GetSeedData()
        {
            //TimeZoneData is seeded from the Initial Migration by running an embedded sql script. 
            //the commented out code below is what should be in the initial Migration class

            /*
             * private static void SeedTimeZoneData(MigrationBuilder migrationBuilder)
               {
                   var resourceName = "Apprendi.Infrastructure.Persistence.Configurations.TimeZoneData.time_zone.sql";
                   using var stream = typeof(Migration).Assembly.GetManifestResourceStream(resourceName);
                   using var reader = new StreamReader(stream);
                   var sql = reader.ReadToEnd();
                   migrationBuilder.Sql(sql);
               }

               protected override void Up(MigrationBuilder migrationBuilder)
               {
                   SeedTimeZoneData(migrationBuilder);
                   ...
                   ...
               }
             */
            yield break;

            //var resourceName = "Apprendi.Infrastructure.Persistence.Configurations.TimeZoneData.time_zone.csv";
            //var assembly = typeof(TimeZoneDataConfiguration).Assembly;

            //var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) 
            //{ 
            //    HasHeaderRecord = false 
            //};
            //using var stream = assembly.GetManifestResourceStream(resourceName);
            //using var reader = new StreamReader(stream);
            //using var csv = new CsvReader(reader, configuration);

            //while (csv.Read()) 
            //{
            //    var timeZoneData = new TimeZoneData
            //    {
            //        ZoneName = csv.GetField<string>(0),
            //        CountryCode = csv.GetField<string>(1),
            //        Abbreviation = csv.GetField<string>(2),
            //        TimeStart = csv.GetField<long>(3),
            //        GmtOffset = csv.GetField<long>(4),
            //        DaylightSavingTime = csv.GetField<string>(5),                    
            //    };

            //    timeZoneData.Id = GetId(timeZoneData);

            //    yield return timeZoneData;
            //}
        }
    }
}

