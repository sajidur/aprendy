using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apprendi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpokenLanguage_LanguageProficiencyLevels_ProficiencyLevelId",
                table: "SpokenLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_SpokenLanguage_Languages_LanguageId",
                table: "SpokenLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_SpokenLanguage_Tutors_TutorId",
                table: "SpokenLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpokenLanguage",
                table: "SpokenLanguage");

            migrationBuilder.RenameTable(
                name: "SpokenLanguage",
                newName: "SpokenLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_SpokenLanguage_ProficiencyLevelId",
                table: "SpokenLanguages",
                newName: "IX_SpokenLanguages_ProficiencyLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_SpokenLanguage_LanguageId",
                table: "SpokenLanguages",
                newName: "IX_SpokenLanguages_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpokenLanguages",
                table: "SpokenLanguages",
                columns: new[] { "TutorId", "LanguageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpokenLanguages_LanguageProficiencyLevels_ProficiencyLevelId",
                table: "SpokenLanguages",
                column: "ProficiencyLevelId",
                principalTable: "LanguageProficiencyLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpokenLanguages_Languages_LanguageId",
                table: "SpokenLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpokenLanguages_Tutors_TutorId",
                table: "SpokenLanguages",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpokenLanguages_LanguageProficiencyLevels_ProficiencyLevelId",
                table: "SpokenLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_SpokenLanguages_Languages_LanguageId",
                table: "SpokenLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_SpokenLanguages_Tutors_TutorId",
                table: "SpokenLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpokenLanguages",
                table: "SpokenLanguages");

            migrationBuilder.RenameTable(
                name: "SpokenLanguages",
                newName: "SpokenLanguage");

            migrationBuilder.RenameIndex(
                name: "IX_SpokenLanguages_ProficiencyLevelId",
                table: "SpokenLanguage",
                newName: "IX_SpokenLanguage_ProficiencyLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_SpokenLanguages_LanguageId",
                table: "SpokenLanguage",
                newName: "IX_SpokenLanguage_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpokenLanguage",
                table: "SpokenLanguage",
                columns: new[] { "TutorId", "LanguageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpokenLanguage_LanguageProficiencyLevels_ProficiencyLevelId",
                table: "SpokenLanguage",
                column: "ProficiencyLevelId",
                principalTable: "LanguageProficiencyLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpokenLanguage_Languages_LanguageId",
                table: "SpokenLanguage",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpokenLanguage_Tutors_TutorId",
                table: "SpokenLanguage",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
