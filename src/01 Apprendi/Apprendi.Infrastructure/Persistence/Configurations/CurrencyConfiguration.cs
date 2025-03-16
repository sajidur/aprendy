using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
                .HasKey(currency => currency.Id);

            builder
                .Property(currency => currency.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(currency => currency.Symbol)
                .HasMaxLength(10);

            builder
                .Property(currency => currency.Code)
                .HasMaxLength(10);

            builder
                .HasData(GetSeedData());
        }

        private static IEnumerable<Currency> GetSeedData()
        {
            yield return new Currency
            {
                Id = 1,
                Name = "United States Dollar",
                Code = "USD",
                Symbol = "$",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 2,
                Name = "Euro",
                Code = "EUR",
                Symbol = "€",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 3,
                Name = "Japanese Yen",
                Code = "JPY",
                Symbol = "¥",
                DecimalPlaces = 0
            };

            yield return new Currency
            {
                Id = 4,
                Name = "British Pound Sterling",
                Code = "GBP",
                Symbol = "£",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 5,
                Name = "Swiss Franc",
                Code = "CHF",
                Symbol = "CHF",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 6,
                Name = "Canadian Dollar",
                Code = "CAD",
                Symbol = "$",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 7,
                Name = "Australian Dollar",
                Code = "AUD",
                Symbol = "$",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 8,
                Name = "Chinese Yuan Renminbi",
                Code = "CNY",
                Symbol = "¥",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 9,
                Name = "Hong Kong Dollar",
                Code = "HKD",
                Symbol = "$",
                DecimalPlaces = 2
            };

            yield return new Currency
            {
                Id = 10,
                Name = "New Zealand Dollar",
                Code = "NZD",
                Symbol = "$",
                DecimalPlaces = 2
            };
        }
    }
}

