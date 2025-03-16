using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apprendi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTutorproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTutor");

            migrationBuilder.DropTable(
                name: "TeachingCertificateTutor");

            migrationBuilder.DropTable(
                name: "TeachingMaterialTutor");

            migrationBuilder.DropTable(
                name: "TeachingPreferenceTutor");

            migrationBuilder.CreateTable(
                name: "TutorSubject",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TutorId1 = table.Column<int>(type: "int", nullable: true),
                    SubjectId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubject", x => new { x.TutorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_TutorSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSubject_Subjects_SubjectId1",
                        column: x => x.SubjectId1,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TutorSubject_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSubject_Tutors_TutorId1",
                        column: x => x.TutorId1,
                        principalTable: "Tutors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TutorTeachingCertificate",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    TeachingCertificateId = table.Column<int>(type: "int", nullable: false),
                    TutorId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorTeachingCertificate", x => new { x.TutorId, x.TeachingCertificateId });
                    table.ForeignKey(
                        name: "FK_TutorTeachingCertificate_TeachingCertificates_TeachingCertificateId",
                        column: x => x.TeachingCertificateId,
                        principalTable: "TeachingCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTeachingCertificate_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTeachingCertificate_Tutors_TutorId1",
                        column: x => x.TutorId1,
                        principalTable: "Tutors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TutorTeachingMaterial",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    TeachingMaterialId = table.Column<int>(type: "int", nullable: false),
                    TutorId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorTeachingMaterial", x => new { x.TutorId, x.TeachingMaterialId });
                    table.ForeignKey(
                        name: "FK_TutorTeachingMaterial_TeachingMaterials_TeachingMaterialId",
                        column: x => x.TeachingMaterialId,
                        principalTable: "TeachingMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTeachingMaterial_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTeachingMaterial_Tutors_TutorId1",
                        column: x => x.TutorId1,
                        principalTable: "Tutors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TutorTeachingPreference",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    TeachingPreferenceId = table.Column<int>(type: "int", nullable: false),
                    TutorId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorTeachingPreference", x => new { x.TutorId, x.TeachingPreferenceId });
                    table.ForeignKey(
                        name: "FK_TutorTeachingPreference_TeachingPreferences_TeachingPreferenceId",
                        column: x => x.TeachingPreferenceId,
                        principalTable: "TeachingPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTeachingPreference_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTeachingPreference_Tutors_TutorId1",
                        column: x => x.TutorId1,
                        principalTable: "Tutors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_SubjectId",
                table: "TutorSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_SubjectId1",
                table: "TutorSubject",
                column: "SubjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_TutorId1",
                table: "TutorSubject",
                column: "TutorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTeachingCertificate_TeachingCertificateId",
                table: "TutorTeachingCertificate",
                column: "TeachingCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTeachingCertificate_TutorId1",
                table: "TutorTeachingCertificate",
                column: "TutorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTeachingMaterial_TeachingMaterialId",
                table: "TutorTeachingMaterial",
                column: "TeachingMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTeachingMaterial_TutorId1",
                table: "TutorTeachingMaterial",
                column: "TutorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTeachingPreference_TeachingPreferenceId",
                table: "TutorTeachingPreference",
                column: "TeachingPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTeachingPreference_TutorId1",
                table: "TutorTeachingPreference",
                column: "TutorId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorSubject");

            migrationBuilder.DropTable(
                name: "TutorTeachingCertificate");

            migrationBuilder.DropTable(
                name: "TutorTeachingMaterial");

            migrationBuilder.DropTable(
                name: "TutorTeachingPreference");

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
                        name: "FK_TeachingCertificateTutor_TeachingCertificates_TeachingCertificatesId",
                        column: x => x.TeachingCertificatesId,
                        principalTable: "TeachingCertificates",
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
                        name: "FK_TeachingMaterialTutor_TeachingMaterials_TeachingMaterialsId",
                        column: x => x.TeachingMaterialsId,
                        principalTable: "TeachingMaterials",
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
                name: "IX_TeachingPreferenceTutor_TutorId",
                table: "TeachingPreferenceTutor",
                column: "TutorId");
        }
    }
}
