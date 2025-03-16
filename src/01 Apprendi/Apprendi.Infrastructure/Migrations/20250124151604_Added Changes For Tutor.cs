using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apprendi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedChangesForTutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorSubject_Users_TutorId",
                table: "TutorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LanguageDetail");

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

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SignUpStage = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "About"),
                    CountryOfBirthId = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    IsOver18 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutors_Countries_CountryOfBirthId",
                        column: x => x.CountryOfBirthId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tutors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorLanguage_LanguageId",
                table: "TutorLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorLanguage_ProficiencyLevelId",
                table: "TutorLanguage",
                column: "ProficiencyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_CountryOfBirthId",
                table: "Tutors",
                column: "CountryOfBirthId");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorSubject_Tutors_TutorId",
                table: "TutorSubject",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorSubject_Tutors_TutorId",
                table: "TutorSubject");

            migrationBuilder.DropTable(
                name: "TutorLanguage");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TutorSubject_Users_TutorId",
                table: "TutorSubject",
                column: "TutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
