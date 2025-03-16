using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Apprendi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorLanguage");

            migrationBuilder.DropTable(
                name: "TutorSubject");

            migrationBuilder.AlterColumn<string>(
                name: "SignUpStage",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "PersonalInformation",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "About");

            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "Tutors",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryResidencyId",
                table: "Tutors",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Tutors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsOtherCertificateSpecified",
                table: "Tutors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhotoPolicyAgreed",
                table: "Tutors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Tutors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCertificates",
                table: "Tutors",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoProfileFileName",
                table: "Tutors",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeachingExperienceInYears",
                table: "Tutors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoIntroductionFileName",
                table: "Tutors",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpokenLanguage",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpokenLanguage", x => new { x.TutorId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SpokenLanguage_LanguageProficiencyLevels_ProficiencyLevelId",
                        column: x => x.ProficiencyLevelId,
                        principalTable: "LanguageProficiencyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpokenLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpokenLanguage_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTutor",
                columns: table => new
                {
                    TeachingSubjectsId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTutor", x => new { x.TeachingSubjectsId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_SubjectTutor_Subjects_TeachingSubjectsId",
                        column: x => x.TeachingSubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTutor_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingCertificate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingCertificate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachingMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachingPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingPreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachingCertificateTutor",
                columns: table => new
                {
                    TeachingCertificatesId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingCertificateTutor", x => new { x.TeachingCertificatesId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_TeachingCertificateTutor_TeachingCertificate_TeachingCertificatesId",
                        column: x => x.TeachingCertificatesId,
                        principalTable: "TeachingCertificate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingCertificateTutor_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingMaterialTutor",
                columns: table => new
                {
                    TeachingMaterialsId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingMaterialTutor", x => new { x.TeachingMaterialsId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_TeachingMaterialTutor_TeachingMaterial_TeachingMaterialsId",
                        column: x => x.TeachingMaterialsId,
                        principalTable: "TeachingMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingMaterialTutor_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingPreferencesTutor",
                columns: table => new
                {
                    TeachingPreferencesId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingPreferencesTutor", x => new { x.TeachingPreferencesId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_TeachingPreferencesTutor_TeachingPreferences_TeachingPreferencesId",
                        column: x => x.TeachingPreferencesId,
                        principalTable: "TeachingPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingPreferencesTutor_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TeachingCertificate",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Teaching English to Speakers of Other Languages - a certification for teaching English globally.", "TESOL" },
                    { 2, "Teaching English as a Foreign Language - widely accepted qualification for teaching English abroad.", "TEFL" },
                    { 3, "Certificate in English Language Teaching to Adults - awarded by Cambridge English.", "CELTA" },
                    { 4, "Diploma in Teaching English to Speakers of Other Languages - an advanced qualification.", "DELTA" },
                    { 5, "Certification for professionals training students for the International English Language Testing System.", "IELTS Trainer" },
                    { 6, "American Council on the Teaching of Foreign Languages - proficiency-based teaching certification.", "ACTFL" }
                });

            migrationBuilder.InsertData(
                table: "TeachingMaterial",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PDF File" },
                    { 2, "Audio Files" },
                    { 3, "Flashcards" },
                    { 4, "Test Templates and Examples" },
                    { 5, "Text Documents" },
                    { 6, "Image Files" },
                    { 7, "Articles and News" },
                    { 8, "Graphs and Charts" },
                    { 9, "Presentation Slides / PowerPoint" },
                    { 10, "Video Files" },
                    { 11, "Quizzes" },
                    { 12, "Homework Assignments" }
                });

            migrationBuilder.InsertData(
                table: "TeachingPreferences",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Films and TV Series" },
                    { 3, "Art" },
                    { 4, "Business and Finance" },
                    { 5, "Pets and Animals" },
                    { 6, "Legal Services" },
                    { 7, "Environment and Nature" },
                    { 8, "Sports and Fitness" },
                    { 9, "Reading" },
                    { 10, "History" },
                    { 11, "Medical and Healthcare" },
                    { 12, "Gaming" },
                    { 13, "Marketing" },
                    { 14, "Animation and Comics" },
                    { 15, "Food" },
                    { 16, "Writing" },
                    { 17, "Science" },
                    { 18, "Technology" },
                    { 19, "Travel" },
                    { 20, "Fashion and Beauty" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_CountryResidencyId",
                table: "Tutors",
                column: "CountryResidencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SpokenLanguage_LanguageId",
                table: "SpokenLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SpokenLanguage_ProficiencyLevelId",
                table: "SpokenLanguage",
                column: "ProficiencyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTutor_TutorId",
                table: "SubjectTutor",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingCertificateTutor_TutorId",
                table: "TeachingCertificateTutor",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingMaterialTutor_TutorId",
                table: "TeachingMaterialTutor",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPreferencesTutor_TutorId",
                table: "TeachingPreferencesTutor",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutors_Countries_CountryResidencyId",
                table: "Tutors",
                column: "CountryResidencyId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutors_Countries_CountryResidencyId",
                table: "Tutors");

            migrationBuilder.DropTable(
                name: "SpokenLanguage");

            migrationBuilder.DropTable(
                name: "SubjectTutor");

            migrationBuilder.DropTable(
                name: "TeachingCertificateTutor");

            migrationBuilder.DropTable(
                name: "TeachingMaterialTutor");

            migrationBuilder.DropTable(
                name: "TeachingPreferencesTutor");

            migrationBuilder.DropTable(
                name: "TeachingCertificate");

            migrationBuilder.DropTable(
                name: "TeachingMaterial");

            migrationBuilder.DropTable(
                name: "TeachingPreferences");

            migrationBuilder.DropIndex(
                name: "IX_Tutors_CountryResidencyId",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "CountryResidencyId",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "IsOtherCertificateSpecified",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "IsPhotoPolicyAgreed",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "OtherCertificates",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "PhotoProfileFileName",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "TeachingExperienceInYears",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "VideoIntroductionFileName",
                table: "Tutors");

            migrationBuilder.AlterColumn<string>(
                name: "SignUpStage",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "About",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "PersonalInformation");

            migrationBuilder.CreateTable(
                name: "TutorLanguage",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorLanguage", x => new { x.TutorId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_TutorLanguage_LanguageProficiencyLevels_ProficiencyLevelId",
                        column: x => x.ProficiencyLevelId,
                        principalTable: "LanguageProficiencyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutorLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutorLanguage_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_TutorSubject_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorLanguage_LanguageId",
                table: "TutorLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorLanguage_ProficiencyLevelId",
                table: "TutorLanguage",
                column: "ProficiencyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_TutorId",
                table: "TutorSubject",
                column: "TutorId");
        }
    }
}
