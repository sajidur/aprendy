using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .Ignore(country => country.CountryName);

            builder
                .HasKey(country => country.Id);

            builder
                .Property(country => country.Id)
                .HasMaxLength(2)
                .IsRequired();

            builder
                .Property(country => country.CountryName)
                .HasMaxLength(45);

            builder
                .HasMany(country => country.TimeZones)
                .WithOne(timeZone => timeZone.Country)
                .HasForeignKey(timeZone => timeZone.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasData(GetSeedData());
        }

        private static IEnumerable<Country> GetSeedData()
        {
            yield return new Country { Id = "CI", CountryName = "Côte d’Ivoire" };
            yield return new Country { Id = "GH", CountryName = "Ghana" };
            yield return new Country { Id = "ET", CountryName = "Ethiopia" };
            yield return new Country { Id = "DZ", CountryName = "Algeria" };
            yield return new Country { Id = "ML", CountryName = "Mali" };
            yield return new Country { Id = "CF", CountryName = "Central African Republic" };
            yield return new Country { Id = "GM", CountryName = "Gambia" };
            yield return new Country { Id = "GW", CountryName = "Guinea-Bissau" };
            yield return new Country { Id = "MW", CountryName = "Malawi" };
            yield return new Country { Id = "CG", CountryName = "Congo - Brazzaville" };
            yield return new Country { Id = "BI", CountryName = "Burundi" };
            yield return new Country { Id = "EG", CountryName = "Egypt" };
            yield return new Country { Id = "MA", CountryName = "Morocco" };
            yield return new Country { Id = "ES", CountryName = "Spain" };
            yield return new Country { Id = "GN", CountryName = "Guinea" };
            yield return new Country { Id = "SN", CountryName = "Senegal" };
            yield return new Country { Id = "TZ", CountryName = "Tanzania" };
            yield return new Country { Id = "DJ", CountryName = "Djibouti" };
            yield return new Country { Id = "CM", CountryName = "Cameroon" };
            yield return new Country { Id = "EH", CountryName = "Western Sahara" };
            yield return new Country { Id = "SL", CountryName = "Sierra Leone" };
            yield return new Country { Id = "BW", CountryName = "Botswana" };
            yield return new Country { Id = "ZW", CountryName = "Zimbabwe" };
            yield return new Country { Id = "ZA", CountryName = "South Africa" };
            yield return new Country { Id = "SS", CountryName = "South Sudan" };
            yield return new Country { Id = "UG", CountryName = "Uganda" };
            yield return new Country { Id = "SD", CountryName = "Sudan" };
            yield return new Country { Id = "RW", CountryName = "Rwanda" };
            yield return new Country { Id = "CD", CountryName = "Congo - Kinshasa" };
            yield return new Country { Id = "NG", CountryName = "Nigeria" };
            yield return new Country { Id = "GA", CountryName = "Gabon" };
            yield return new Country { Id = "TG", CountryName = "Togo" };
            yield return new Country { Id = "AO", CountryName = "Angola" };
            yield return new Country { Id = "ZM", CountryName = "Zambia" };
            yield return new Country { Id = "GQ", CountryName = "Equatorial Guinea" };
            yield return new Country { Id = "MZ", CountryName = "Mozambique" };
            yield return new Country { Id = "LS", CountryName = "Lesotho" };
            yield return new Country { Id = "SZ", CountryName = "Eswatini" };
            yield return new Country { Id = "SO", CountryName = "Somalia" };
            yield return new Country { Id = "LR", CountryName = "Liberia" };
            yield return new Country { Id = "KE", CountryName = "Kenya" };
            yield return new Country { Id = "TD", CountryName = "Chad" };
            yield return new Country { Id = "NE", CountryName = "Niger" };
            yield return new Country { Id = "MR", CountryName = "Mauritania" };
            yield return new Country { Id = "BF", CountryName = "Burkina Faso" };
            yield return new Country { Id = "BJ", CountryName = "Benin" };
            yield return new Country { Id = "ST", CountryName = "São Tomé & Príncipe" };
            yield return new Country { Id = "LY", CountryName = "Libya" };
            yield return new Country { Id = "TN", CountryName = "Tunisia" };
            yield return new Country { Id = "NA", CountryName = "Namibia" };
            yield return new Country { Id = "US", CountryName = "United States" };
            yield return new Country { Id = "AI", CountryName = "Anguilla" };
            yield return new Country { Id = "AG", CountryName = "Antigua & Barbuda" };
            yield return new Country { Id = "BR", CountryName = "Brazil" };
            yield return new Country { Id = "AR", CountryName = "Argentina" };
            yield return new Country { Id = "AW", CountryName = "Aruba" };
            yield return new Country { Id = "PY", CountryName = "Paraguay" };
            yield return new Country { Id = "MX", CountryName = "Mexico" };
            yield return new Country { Id = "BB", CountryName = "Barbados" };
            yield return new Country { Id = "BZ", CountryName = "Belize" };
            yield return new Country { Id = "CA", CountryName = "Canada" };
            yield return new Country { Id = "CO", CountryName = "Colombia" };
            yield return new Country { Id = "VE", CountryName = "Venezuela" };
            yield return new Country { Id = "GF", CountryName = "French Guiana" };
            yield return new Country { Id = "KY", CountryName = "Cayman Islands" };
            yield return new Country { Id = "CR", CountryName = "Costa Rica" };
            yield return new Country { Id = "CW", CountryName = "Curaçao" };
            yield return new Country { Id = "GL", CountryName = "Greenland" };
            yield return new Country { Id = "DM", CountryName = "Dominica" };
            yield return new Country { Id = "SV", CountryName = "El Salvador" };
            yield return new Country { Id = "TC", CountryName = "Turks & Caicos Islands" };
            yield return new Country { Id = "GD", CountryName = "Grenada" };
            yield return new Country { Id = "GP", CountryName = "Guadeloupe" };
            yield return new Country { Id = "GT", CountryName = "Guatemala" };
            yield return new Country { Id = "EC", CountryName = "Ecuador" };
            yield return new Country { Id = "GY", CountryName = "Guyana" };
            yield return new Country { Id = "CU", CountryName = "Cuba" };
            yield return new Country { Id = "JM", CountryName = "Jamaica" };
            yield return new Country { Id = "BQ", CountryName = "Caribbean Netherlands" };
            yield return new Country { Id = "BO", CountryName = "Bolivia" };
            yield return new Country { Id = "PE", CountryName = "Peru" };
            yield return new Country { Id = "SX", CountryName = "Sint Maarten" };
            yield return new Country { Id = "NI", CountryName = "Nicaragua" };
            yield return new Country { Id = "MF", CountryName = "St. Martin" };
            yield return new Country { Id = "MQ", CountryName = "Martinique" };
            yield return new Country { Id = "PM", CountryName = "St. Pierre & Miquelon" };
            yield return new Country { Id = "UY", CountryName = "Uruguay" };
            yield return new Country { Id = "MS", CountryName = "Montserrat" };
            yield return new Country { Id = "BS", CountryName = "Bahamas" };
            yield return new Country { Id = "PA", CountryName = "Panama" };
            yield return new Country { Id = "SR", CountryName = "Suriname" };
            yield return new Country { Id = "HT", CountryName = "Haiti" };
            yield return new Country { Id = "TT", CountryName = "Trinidad & Tobago" };
            yield return new Country { Id = "PR", CountryName = "Puerto Rico" };
            yield return new Country { Id = "CL", CountryName = "Chile" };
            yield return new Country { Id = "DO", CountryName = "Dominican Republic" };
            yield return new Country { Id = "BL", CountryName = "St. Barthélemy" };
            yield return new Country { Id = "KN", CountryName = "St. Kitts & Nevis" };
            yield return new Country { Id = "LC", CountryName = "St. Lucia" };
            yield return new Country { Id = "VI", CountryName = "U.S. Virgin Islands" };
            yield return new Country { Id = "VC", CountryName = "St. Vincent & Grenadines" };
            yield return new Country { Id = "HN", CountryName = "Honduras" };
            yield return new Country { Id = "VG", CountryName = "British Virgin Islands" };
            yield return new Country { Id = "AQ", CountryName = "Antarctica" };
            yield return new Country { Id = "AU", CountryName = "Australia" };
            yield return new Country { Id = "SJ", CountryName = "Svalbard & Jan Mayen" };
            yield return new Country { Id = "YE", CountryName = "Yemen" };
            yield return new Country { Id = "KZ", CountryName = "Kazakhstan" };
            yield return new Country { Id = "JO", CountryName = "Jordan" };
            yield return new Country { Id = "RU", CountryName = "Russia" };
            yield return new Country { Id = "TM", CountryName = "Turkmenistan" };
            yield return new Country { Id = "IQ", CountryName = "Iraq" };
            yield return new Country { Id = "BH", CountryName = "Bahrain" };
            yield return new Country { Id = "AZ", CountryName = "Azerbaijan" };
            yield return new Country { Id = "TH", CountryName = "Thailand" };
            yield return new Country { Id = "LB", CountryName = "Lebanon" };
            yield return new Country { Id = "KG", CountryName = "Kyrgyzstan" };
            yield return new Country { Id = "BN", CountryName = "Brunei" };
            yield return new Country { Id = "LK", CountryName = "Sri Lanka" };
            yield return new Country { Id = "SY", CountryName = "Syria" };
            yield return new Country { Id = "BD", CountryName = "Bangladesh" };
            yield return new Country { Id = "TL", CountryName = "Timor-Leste" };
            yield return new Country { Id = "AE", CountryName = "United Arab Emirates" };
            yield return new Country { Id = "TJ", CountryName = "Tajikistan" };
            yield return new Country { Id = "CY", CountryName = "Cyprus" };
            yield return new Country { Id = "PS", CountryName = "Palestinian Territories" };
            yield return new Country { Id = "HK", CountryName = "Hong Kong SAR China" };
            yield return new Country { Id = "MN", CountryName = "Mongolia" };
            yield return new Country { Id = "ID", CountryName = "Indonesia" };
            yield return new Country { Id = "IL", CountryName = "Israel" };
            yield return new Country { Id = "AF", CountryName = "Afghanistan" };
            yield return new Country { Id = "PK", CountryName = "Pakistan" };
            yield return new Country { Id = "MY", CountryName = "Malaysia" };
            yield return new Country { Id = "KW", CountryName = "Kuwait" };
            yield return new Country { Id = "MO", CountryName = "Macao SAR China" };
            yield return new Country { Id = "PH", CountryName = "Philippines" };
            yield return new Country { Id = "OM", CountryName = "Oman" };
            yield return new Country { Id = "KH", CountryName = "Cambodia" };
            yield return new Country { Id = "KP", CountryName = "North Korea" };
            yield return new Country { Id = "QA", CountryName = "Qatar" };
            yield return new Country { Id = "SA", CountryName = "Saudi Arabia" };
            yield return new Country { Id = "UZ", CountryName = "Uzbekistan" };
            yield return new Country { Id = "KR", CountryName = "South Korea" };
            yield return new Country { Id = "CN", CountryName = "China" };
            yield return new Country { Id = "SG", CountryName = "Singapore" };
            yield return new Country { Id = "TW", CountryName = "Taiwan" };
            yield return new Country { Id = "GE", CountryName = "Georgia" };
            yield return new Country { Id = "IR", CountryName = "Iran" };
            yield return new Country { Id = "BT", CountryName = "Bhutan" };
            yield return new Country { Id = "JP", CountryName = "Japan" };
            yield return new Country { Id = "LA", CountryName = "Laos" };
            yield return new Country { Id = "AM", CountryName = "Armenia" };
            yield return new Country { Id = "PT", CountryName = "Portugal" };
            yield return new Country { Id = "BM", CountryName = "Bermuda" };
            yield return new Country { Id = "CV", CountryName = "Cape Verde" };
            yield return new Country { Id = "IS", CountryName = "Iceland" };
            yield return new Country { Id = "GS", CountryName = "South Georgia & South Sandwich Islands" };
            yield return new Country { Id = "TA", CountryName = "Tristan da Cunha" };
            yield return new Country { Id = "FK", CountryName = "Falkland Islands" };
            yield return new Country { Id = "NL", CountryName = "Netherlands" };
            yield return new Country { Id = "AD", CountryName = "Andorra" };
            yield return new Country { Id = "GR", CountryName = "Greece" };
            yield return new Country { Id = "RS", CountryName = "Serbia" };
            yield return new Country { Id = "DE", CountryName = "Germany" };
            yield return new Country { Id = "SK", CountryName = "Slovakia" };
            yield return new Country { Id = "BE", CountryName = "Belgium" };
            yield return new Country { Id = "RO", CountryName = "Romania" };
            yield return new Country { Id = "HU", CountryName = "Hungary" };
            yield return new Country { Id = "MD", CountryName = "Moldova" };
            yield return new Country { Id = "DK", CountryName = "Denmark" };
            yield return new Country { Id = "IE", CountryName = "Ireland" };
            yield return new Country { Id = "GI", CountryName = "Gibraltar" };
            yield return new Country { Id = "GG", CountryName = "Guernsey" };
            yield return new Country { Id = "FI", CountryName = "Finland" };
            yield return new Country { Id = "IM", CountryName = "Isle of Man" };
            yield return new Country { Id = "TR", CountryName = "Turkey" };
            yield return new Country { Id = "JE", CountryName = "Jersey" };
            yield return new Country { Id = "SI", CountryName = "Slovenia" };
            yield return new Country { Id = "GB", CountryName = "United Kingdom" };
            yield return new Country { Id = "LU", CountryName = "Luxembourg" };
            yield return new Country { Id = "MT", CountryName = "Malta" };
            yield return new Country { Id = "AX", CountryName = "Åland Islands" };
            yield return new Country { Id = "BY", CountryName = "Belarus" };
            yield return new Country { Id = "MC", CountryName = "Monaco" };
            yield return new Country { Id = "NO", CountryName = "Norway" };
            yield return new Country { Id = "FR", CountryName = "France" };
            yield return new Country { Id = "ME", CountryName = "Montenegro" };
            yield return new Country { Id = "CZ", CountryName = "Czechia" };
            yield return new Country { Id = "LV", CountryName = "Latvia" };
            yield return new Country { Id = "IT", CountryName = "Italy" };
            yield return new Country { Id = "SM", CountryName = "San Marino" };
            yield return new Country { Id = "BA", CountryName = "Bosnia & Herzegovina" };
            yield return new Country { Id = "UA", CountryName = "Ukraine" };
            yield return new Country { Id = "MK", CountryName = "North Macedonia" };
            yield return new Country { Id = "BG", CountryName = "Bulgaria" };
            yield return new Country { Id = "SE", CountryName = "Sweden" };
            yield return new Country { Id = "EE", CountryName = "Estonia" };
            yield return new Country { Id = "AL", CountryName = "Albania" };
            yield return new Country { Id = "LI", CountryName = "Liechtenstein" };
            yield return new Country { Id = "VA", CountryName = "Vatican City" };
            yield return new Country { Id = "AT", CountryName = "Austria" };
            yield return new Country { Id = "LT", CountryName = "Lithuania" };
            yield return new Country { Id = "PL", CountryName = "Poland" };
            yield return new Country { Id = "HR", CountryName = "Croatia" };
            yield return new Country { Id = "CH", CountryName = "Switzerland" };
            yield return new Country { Id = "MG", CountryName = "Madagascar" };
            yield return new Country { Id = "DG", CountryName = "Diego Garcia" };
            yield return new Country { Id = "CX", CountryName = "Christmas Island" };
            yield return new Country { Id = "CC", CountryName = "Cocos (Keeling) Islands" };
            yield return new Country { Id = "KM", CountryName = "Comoros" };
            yield return new Country { Id = "TF", CountryName = "French Southern Territories" };
            yield return new Country { Id = "SC", CountryName = "Seychelles" };
            yield return new Country { Id = "MV", CountryName = "Maldives" };
            yield return new Country { Id = "MU", CountryName = "Mauritius" };
            yield return new Country { Id = "YT", CountryName = "Mayotte" };
            yield return new Country { Id = "RE", CountryName = "Réunion" };
            yield return new Country { Id = "WS", CountryName = "Samoa" };
            yield return new Country { Id = "NZ", CountryName = "New Zealand" };
            yield return new Country { Id = "PG", CountryName = "Papua New Guinea" };
            yield return new Country { Id = "VU", CountryName = "Vanuatu" };
            yield return new Country { Id = "TK", CountryName = "Tokelau" };
            yield return new Country { Id = "FJ", CountryName = "Fiji" };
            yield return new Country { Id = "TV", CountryName = "Tuvalu" };
            yield return new Country { Id = "PF", CountryName = "French Polynesia" };
            yield return new Country { Id = "SB", CountryName = "Solomon Islands" };
            yield return new Country { Id = "GU", CountryName = "Guam" };
            yield return new Country { Id = "KI", CountryName = "Kiribati" };
            yield return new Country { Id = "FM", CountryName = "Micronesia" };
            yield return new Country { Id = "MH", CountryName = "Marshall Islands" };
            yield return new Country { Id = "UM", CountryName = "U.S. Outlying Islands" };
            yield return new Country { Id = "NR", CountryName = "Nauru" };
            yield return new Country { Id = "NU", CountryName = "Niue" };
            yield return new Country { Id = "NF", CountryName = "Norfolk Island" };
            yield return new Country { Id = "NC", CountryName = "New Caledonia" };
            yield return new Country { Id = "AS", CountryName = "American Samoa" };
            yield return new Country { Id = "PW", CountryName = "Palau" };
            yield return new Country { Id = "PN", CountryName = "Pitcairn Islands" };
            yield return new Country { Id = "CK", CountryName = "Cook Islands" };
            yield return new Country { Id = "MP", CountryName = "Northern Mariana Islands" };
            yield return new Country { Id = "TO", CountryName = "Tonga" };
            yield return new Country { Id = "WF", CountryName = "Wallis & Futuna" };
        }
    }
}

