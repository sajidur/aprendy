using Apprendi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apprendi.Infrastructure.Persistence.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
                .HasKey(subject => subject.Id);

            builder
                .Property(subject => subject.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .HasData(GetSeedData());
        }

        public static IEnumerable<Subject> GetSeedData()
        {
            var i = 1;
            yield return new Subject { Id = i++, Name = "English" };
            yield return new Subject { Id = i++, Name = "Spanish" };
            yield return new Subject { Id = i++, Name = "French" };
            yield return new Subject { Id = i++, Name = "German" };
            yield return new Subject { Id = i++, Name = "Japanese" };
            yield return new Subject { Id = i++, Name = "Italian" };
            yield return new Subject { Id = i++, Name = "Korean" };
            yield return new Subject { Id = i++, Name = "Arabic" };
            yield return new Subject { Id = i++, Name = "Chinese(Mandarin)" };
            yield return new Subject { Id = i++, Name = "Portuguese" };
            yield return new Subject { Id = i++, Name = "Statistics" };
            yield return new Subject { Id = i++, Name = "Computer science" };
            yield return new Subject { Id = i++, Name = "Economics" };
            yield return new Subject { Id = i++, Name = "Chemistry" };
            yield return new Subject { Id = i++, Name = "Biology" };
            yield return new Subject { Id = i++, Name = "Algebra" };
            yield return new Subject { Id = i++, Name = "Physics" };
            yield return new Subject { Id = i++, Name = "History" };
            yield return new Subject { Id = i++, Name = "Math" };
            yield return new Subject { Id = i++, Name = "Accounting" };
            yield return new Subject { Id = i++, Name = "Russian" };
            yield return new Subject { Id = i++, Name = "Polish" };
            yield return new Subject { Id = i++, Name = "Turkish" };
            yield return new Subject { Id = i++, Name = "Dutch" };
            yield return new Subject { Id = i++, Name = "Ukrainian" };
            yield return new Subject { Id = i++, Name = "Armenian" };
            yield return new Subject { Id = i++, Name = "Hindi" };
            yield return new Subject { Id = i++, Name = "Czech" };
            yield return new Subject { Id = i++, Name = "Norwegian" };
            yield return new Subject { Id = i++, Name = "Hebrew" };
            yield return new Subject { Id = i++, Name = "Greek" };
            yield return new Subject { Id = i++, Name = "Finnish" };
            yield return new Subject { Id = i++, Name = "Georgian" };
            yield return new Subject { Id = i++, Name = "Swedish" };
            yield return new Subject { Id = i++, Name = "Hungarian" };
            yield return new Subject { Id = i++, Name = "Arts" };
            yield return new Subject { Id = i++, Name = "Music" };
            yield return new Subject { Id = i++, Name = "Acting skills" };
            yield return new Subject { Id = i++, Name = "Art classes" };
            yield return new Subject { Id = i++, Name = "Bengali" };
            yield return new Subject { Id = i++, Name = "Danish" };
            yield return new Subject { Id = i++, Name = "Thai" };
            yield return new Subject { Id = i++, Name = "Latin" };
            yield return new Subject { Id = i++, Name = "Khmer" };
            yield return new Subject { Id = i++, Name = "Belarusian" };
            yield return new Subject { Id = i++, Name = "Urdu" };
            yield return new Subject { Id = i++, Name = "Sanskrit" };
            yield return new Subject { Id = i++, Name = "Punjabi" };
            yield return new Subject { Id = i++, Name = "Tibetan" };
            yield return new Subject { Id = i++, Name = "Lithuanian" };
            yield return new Subject { Id = i++, Name = "Slovak" };
            yield return new Subject { Id = i++, Name = "Serbian" };
            yield return new Subject { Id = i++, Name = "Vietnamese" };
            yield return new Subject { Id = i++, Name = "Telugu" };
            yield return new Subject { Id = i++, Name = "Tamil" };
            yield return new Subject { Id = i++, Name = "Sign" };
            yield return new Subject { Id = i++, Name = "Tagalog" };
            yield return new Subject { Id = i++, Name = "Romanian" };
            yield return new Subject { Id = i++, Name = "Irish" };
            yield return new Subject { Id = i++, Name = "Icelandic" };
            yield return new Subject { Id = i++, Name = "Persian (Farsi)" };
            yield return new Subject { Id = i++, Name = "Croatian" };
            yield return new Subject { Id = i++, Name = "Catalan" };
            yield return new Subject { Id = i++, Name = "Bulgarian" };
            yield return new Subject { Id = i++, Name = "International Business" };
            yield return new Subject { Id = i++, Name = "Marketing Strategy" };
            yield return new Subject { Id = i++, Name = "Content marketing" };
            yield return new Subject { Id = i++, Name = "Business & Management" };
            yield return new Subject { Id = i++, Name = "Dota 2" };
            yield return new Subject { Id = i++, Name = "Concursos" };
            yield return new Subject { Id = i++, Name = "Objective C" };
            yield return new Subject { Id = i++, Name = "Data Science" };
            yield return new Subject { Id = i++, Name = "UX/UI" };
            yield return new Subject { Id = i++, Name = "IT Project Management" };
            yield return new Subject { Id = i++, Name = "Artificial intelligence" };
            yield return new Subject { Id = i++, Name = "Web Development" };
            yield return new Subject { Id = i++, Name = "Web Analytics" };
            yield return new Subject { Id = i++, Name = "Java" };
            yield return new Subject { Id = i++, Name = "C" };
            yield return new Subject { Id = i++, Name = "Swift" };
            yield return new Subject { Id = i++, Name = "Go language" };
            yield return new Subject { Id = i++, Name = "Rust" };
            yield return new Subject { Id = i++, Name = "Scala" };
            yield return new Subject { Id = i++, Name = "HTML" };
            yield return new Subject { Id = i++, Name = "XML" };
            yield return new Subject { Id = i++, Name = "CSS" };
            yield return new Subject { Id = i++, Name = "JavaScript" };
            yield return new Subject { Id = i++, Name = "NodeJS" };
            yield return new Subject { Id = i++, Name = "Python" };
            yield return new Subject { Id = i++, Name = "PHP" };
            yield return new Subject { Id = i++, Name = "С++" };
            yield return new Subject { Id = i++, Name = "Bash" };
            yield return new Subject { Id = i++, Name = "iOS app development" };
            yield return new Subject { Id = i++, Name = "Android app development" };
            yield return new Subject { Id = i++, Name = "Databases" };
            yield return new Subject { Id = i++, Name = "Algorithms" };
            yield return new Subject { Id = i++, Name = "Marathi" };
            yield return new Subject { Id = i++, Name = "Yoruba" };
            yield return new Subject { Id = i++, Name = "Amharic" };
            yield return new Subject { Id = i++, Name = "Maori" };
            yield return new Subject { Id = i++, Name = "Igbo" };
            yield return new Subject { Id = i++, Name = "Sinhala" };
            yield return new Subject { Id = i++, Name = "Burmese" };
            yield return new Subject { Id = i++, Name = "Lao" };
            yield return new Subject { Id = i++, Name = "Kazakh" };
            yield return new Subject { Id = i++, Name = "Tamazight" };
            yield return new Subject { Id = i++, Name = "Public Speaking" };
            yield return new Subject { Id = i++, Name = "Graphic design" };
            yield return new Subject { Id = i++, Name = "Ancient Greek" };
            yield return new Subject { Id = i++, Name = "Ruby" };
            yield return new Subject { Id = i++, Name = "С#" };
            yield return new Subject { Id = i++, Name = "Welsh" };
            yield return new Subject { Id = i++, Name = "Kannada" };
            yield return new Subject { Id = i++, Name = "Gujarati" };
            yield return new Subject { Id = i++, Name = "Maltese" };
            yield return new Subject { Id = i++, Name = "Creole" };
            yield return new Subject { Id = i++, Name = "Yiddish" };
            yield return new Subject { Id = i++, Name = "Bosnian" };
            yield return new Subject { Id = i++, Name = "Estonian" };
            yield return new Subject { Id = i++, Name = "Luganda" };
            yield return new Subject { Id = i++, Name = "Cebuano" };
            yield return new Subject { Id = i++, Name = "Basque" };
            yield return new Subject { Id = i++, Name = "Quichua" };
            yield return new Subject { Id = i++, Name = "Crimean Tatar" };
            yield return new Subject { Id = i++, Name = "Corporate Finance" };
            yield return new Subject { Id = i++, Name = "Geography" };
            yield return new Subject { Id = i++, Name = "Philosophy" };
            yield return new Subject { Id = i++, Name = "Writing" };
            yield return new Subject { Id = i++, Name = "Sociology" };
            yield return new Subject { Id = i++, Name = "Psychology" };
            yield return new Subject { Id = i++, Name = "Social Sciences & Humanities" };
            yield return new Subject { Id = i++, Name = "Motion design" };
            yield return new Subject { Id = i++, Name = "Photography" };
            yield return new Subject { Id = i++, Name = "Malayalam" };
            yield return new Subject { Id = i++, Name = "R" };
            yield return new Subject { Id = i++, Name = "PPC" };
            yield return new Subject { Id = i++, Name = "Albanian" };
            yield return new Subject { Id = i++, Name = "Pashto" };
            yield return new Subject { Id = i++, Name = "Hawaiian" };
            yield return new Subject { Id = i++, Name = "Esperanto" };
            yield return new Subject { Id = i++, Name = "Xhosa" };
            yield return new Subject { Id = i++, Name = "Macedonian" };
            yield return new Subject { Id = i++, Name = "Kurdish" };
            yield return new Subject { Id = i++, Name = "Kinyarwanda" };
            yield return new Subject { Id = i++, Name = "Uzbek" };
            yield return new Subject { Id = i++, Name = "Geometry" };
            yield return new Subject { Id = i++, Name = "Literature" };
            yield return new Subject { Id = i++, Name = "3D design" };
            yield return new Subject { Id = i++, Name = "Video post-production" };
            yield return new Subject { Id = i++, Name = "Tests" };
            yield return new Subject { Id = i++, Name = "Law" };
            yield return new Subject { Id = i++, Name = "Swahili" };
            yield return new Subject { Id = i++, Name = "Afrikaans" };
            yield return new Subject { Id = i++, Name = "Malay" };
            yield return new Subject { Id = i++, Name = "Somali" };
            yield return new Subject { Id = i++, Name = "Slovenian" };
            yield return new Subject { Id = i++, Name = "Latvian" };
            yield return new Subject { Id = i++, Name = "Mongolian" };
            yield return new Subject { Id = i++, Name = "Luxembourgish" };
            yield return new Subject { Id = i++, Name = "Quechua" };
            yield return new Subject { Id = i++, Name = "Azerbaijani" };
            yield return new Subject { Id = i++, Name = "Cantonese" };
            yield return new Subject { Id = i++, Name = "Indonesian" };
            yield return new Subject { Id = i++, Name = "Sales" };
            yield return new Subject { Id = i++, Name = "Business Modelling" };
            yield return new Subject { Id = i++, Name = "Product Management" };
            yield return new Subject { Id = i++, Name = "Business Strategy" };
            yield return new Subject { Id = i++, Name = "Business Analytics" };
            yield return new Subject { Id = i++, Name = "SEO" };
            yield return new Subject { Id = i++, Name = "SMM" };
            yield return new Subject { Id = i++, Name = "Copywriting" };
            yield return new Subject { Id = i++, Name = "Email marketing" };
            yield return new Subject { Id = i++, Name = "PR" };
        }
    }
}

