using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apprendi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingCertificateTutor_TeachingCertificate_TeachingCertificatesId",
                table: "TeachingCertificateTutor");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingMaterialTutor_TeachingMaterial_TeachingMaterialsId",
                table: "TeachingMaterialTutor");

            migrationBuilder.DropTable(
                name: "TeachingPreferencesTutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachingMaterial",
                table: "TeachingMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachingCertificate",
                table: "TeachingCertificate");

            migrationBuilder.RenameTable(
                name: "TeachingMaterial",
                newName: "TeachingMaterials");

            migrationBuilder.RenameTable(
                name: "TeachingCertificate",
                newName: "TeachingCertificates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachingMaterials",
                table: "TeachingMaterials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachingCertificates",
                table: "TeachingCertificates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeachingPreferenceTutor",
                columns: table => new
                {
                    TeachingPreferencesId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingPreferenceTutor", x => new { x.TeachingPreferencesId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_TeachingPreferenceTutor_TeachingPreferences_TeachingPreferencesId",
                        column: x => x.TeachingPreferencesId,
                        principalTable: "TeachingPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingPreferenceTutor_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPreferenceTutor_TutorId",
                table: "TeachingPreferenceTutor",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingCertificateTutor_TeachingCertificates_TeachingCertificatesId",
                table: "TeachingCertificateTutor",
                column: "TeachingCertificatesId",
                principalTable: "TeachingCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingMaterialTutor_TeachingMaterials_TeachingMaterialsId",
                table: "TeachingMaterialTutor",
                column: "TeachingMaterialsId",
                principalTable: "TeachingMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingCertificateTutor_TeachingCertificates_TeachingCertificatesId",
                table: "TeachingCertificateTutor");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingMaterialTutor_TeachingMaterials_TeachingMaterialsId",
                table: "TeachingMaterialTutor");

            migrationBuilder.DropTable(
                name: "TeachingPreferenceTutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachingMaterials",
                table: "TeachingMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachingCertificates",
                table: "TeachingCertificates");

            migrationBuilder.RenameTable(
                name: "TeachingMaterials",
                newName: "TeachingMaterial");

            migrationBuilder.RenameTable(
                name: "TeachingCertificates",
                newName: "TeachingCertificate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachingMaterial",
                table: "TeachingMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachingCertificate",
                table: "TeachingCertificate",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_TeachingPreferencesTutor_TutorId",
                table: "TeachingPreferencesTutor",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingCertificateTutor_TeachingCertificate_TeachingCertificatesId",
                table: "TeachingCertificateTutor",
                column: "TeachingCertificatesId",
                principalTable: "TeachingCertificate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingMaterialTutor_TeachingMaterial_TeachingMaterialsId",
                table: "TeachingMaterialTutor",
                column: "TeachingMaterialsId",
                principalTable: "TeachingMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
