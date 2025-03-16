using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Apprendi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedentitiestosupportTutors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Users",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsOver18",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LanguageProficiencyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageProficiencyLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageDetail",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageDetail", x => new { x.TutorId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_LanguageDetail_LanguageProficiencyLevels_ProficiencyLevelId",
                        column: x => x.ProficiencyLevelId,
                        principalTable: "LanguageProficiencyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguageDetail_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguageDetail_Users_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSubject",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubject", x => new { x.SubjectId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_TutorSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSubject_Users_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LanguageProficiencyLevels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A1" },
                    { 2, "A2" },
                    { 3, "B1" },
                    { 4, "B2" },
                    { 5, "C1" },
                    { 6, "C2" },
                    { 7, "Native" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Spanish" },
                    { 3, "French" },
                    { 4, "German" },
                    { 5, "Japanese" },
                    { 6, "Italian" },
                    { 7, "Korean" },
                    { 8, "Arabic" },
                    { 9, "Chinese(Mandarin)" },
                    { 10, "Portuguese" },
                    { 11, "Statistics" },
                    { 12, "Computer science" },
                    { 13, "Economics" },
                    { 14, "Chemistry" },
                    { 15, "Biology" },
                    { 16, "Algebra" },
                    { 17, "Physics" },
                    { 18, "History" },
                    { 19, "Math" },
                    { 20, "Accounting" },
                    { 21, "Russian" },
                    { 22, "Polish" },
                    { 23, "Turkish" },
                    { 24, "Dutch" },
                    { 25, "Ukrainian" },
                    { 26, "Armenian" },
                    { 27, "Hindi" },
                    { 28, "Czech" },
                    { 29, "Norwegian" },
                    { 30, "Hebrew" },
                    { 31, "Greek" },
                    { 32, "Finnish" },
                    { 33, "Georgian" },
                    { 34, "Swedish" },
                    { 35, "Hungarian" },
                    { 36, "Arts" },
                    { 37, "Music" },
                    { 38, "Acting skills" },
                    { 39, "Art classes" },
                    { 40, "Bengali" },
                    { 41, "Danish" },
                    { 42, "Thai" },
                    { 43, "Latin" },
                    { 44, "Khmer" },
                    { 45, "Belarusian" },
                    { 46, "Urdu" },
                    { 47, "Sanskrit" },
                    { 48, "Punjabi" },
                    { 49, "Tibetan" },
                    { 50, "Lithuanian" },
                    { 51, "Slovak" },
                    { 52, "Serbian" },
                    { 53, "Vietnamese" },
                    { 54, "Telugu" },
                    { 55, "Tamil" },
                    { 56, "Sign" },
                    { 57, "Tagalog" },
                    { 58, "Romanian" },
                    { 59, "Irish" },
                    { 60, "Icelandic" },
                    { 61, "Persian (Farsi)" },
                    { 62, "Croatian" },
                    { 63, "Catalan" },
                    { 64, "Bulgarian" },
                    { 65, "International Business" },
                    { 66, "Marketing Strategy" },
                    { 67, "Content marketing" },
                    { 68, "Business & Management" },
                    { 69, "Dota 2" },
                    { 70, "Concursos" },
                    { 71, "Objective C" },
                    { 72, "Data Science" },
                    { 73, "UX/UI" },
                    { 74, "IT Project Management" },
                    { 75, "Artificial intelligence" },
                    { 76, "Web Development" },
                    { 77, "Web Analytics" },
                    { 78, "Java" },
                    { 79, "C" },
                    { 80, "Swift" },
                    { 81, "Go language" },
                    { 82, "Rust" },
                    { 83, "Scala" },
                    { 84, "HTML" },
                    { 85, "XML" },
                    { 86, "CSS" },
                    { 87, "JavaScript" },
                    { 88, "NodeJS" },
                    { 89, "Python" },
                    { 90, "PHP" },
                    { 91, "С++" },
                    { 92, "Bash" },
                    { 93, "iOS app development" },
                    { 94, "Android app development" },
                    { 95, "Databases" },
                    { 96, "Algorithms" },
                    { 97, "Marathi" },
                    { 98, "Yoruba" },
                    { 99, "Amharic" },
                    { 100, "Maori" },
                    { 101, "Igbo" },
                    { 102, "Sinhala" },
                    { 103, "Burmese" },
                    { 104, "Lao" },
                    { 105, "Kazakh" },
                    { 106, "Tamazight" },
                    { 107, "Public Speaking" },
                    { 108, "Graphic design" },
                    { 109, "Ancient Greek" },
                    { 110, "Ruby" },
                    { 111, "С#" },
                    { 112, "Welsh" },
                    { 113, "Kannada" },
                    { 114, "Gujarati" },
                    { 115, "Maltese" },
                    { 116, "Creole" },
                    { 117, "Yiddish" },
                    { 118, "Bosnian" },
                    { 119, "Estonian" },
                    { 120, "Luganda" },
                    { 121, "Cebuano" },
                    { 122, "Basque" },
                    { 123, "Quichua" },
                    { 124, "Crimean Tatar" },
                    { 125, "Corporate Finance" },
                    { 126, "Geography" },
                    { 127, "Philosophy" },
                    { 128, "Writing" },
                    { 129, "Sociology" },
                    { 130, "Psychology" },
                    { 131, "Social Sciences & Humanities" },
                    { 132, "Motion design" },
                    { 133, "Photography" },
                    { 134, "Malayalam" },
                    { 135, "R" },
                    { 136, "PPC" },
                    { 137, "Albanian" },
                    { 138, "Pashto" },
                    { 139, "Hawaiian" },
                    { 140, "Esperanto" },
                    { 141, "Xhosa" },
                    { 142, "Macedonian" },
                    { 143, "Kurdish" },
                    { 144, "Kinyarwanda" },
                    { 145, "Uzbek" },
                    { 146, "Geometry" },
                    { 147, "Literature" },
                    { 148, "3D design" },
                    { 149, "Video post-production" },
                    { 150, "Tests" },
                    { 151, "Law" },
                    { 152, "Swahili" },
                    { 153, "Afrikaans" },
                    { 154, "Malay" },
                    { 155, "Somali" },
                    { 156, "Slovenian" },
                    { 157, "Latvian" },
                    { 158, "Mongolian" },
                    { 159, "Luxembourgish" },
                    { 160, "Quechua" },
                    { 161, "Azerbaijani" },
                    { 162, "Cantonese" },
                    { 163, "Indonesian" },
                    { 164, "Sales" },
                    { 165, "Business Modelling" },
                    { 166, "Product Management" },
                    { 167, "Business Strategy" },
                    { 168, "Business Analytics" },
                    { 169, "SEO" },
                    { 170, "SMM" },
                    { 171, "Copywriting" },
                    { 172, "Email marketing" },
                    { 173, "PR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDetail_LanguageId",
                table: "LanguageDetail",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDetail_ProficiencyLevelId",
                table: "LanguageDetail",
                column: "ProficiencyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_TutorId",
                table: "TutorSubject",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LanguageDetail");

            migrationBuilder.DropTable(
                name: "TutorSubject");

            migrationBuilder.DropTable(
                name: "LanguageProficiencyLevels");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsOver18",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "FistName");
        }
    }
}
