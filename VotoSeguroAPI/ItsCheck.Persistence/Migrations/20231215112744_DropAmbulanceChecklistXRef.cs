using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropAmbulanceChecklistXRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmbulanceChecklistXRefs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmbulanceChecklistXRefs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AmbulanceId = table.Column<int>(type: "integer", nullable: false),
                    ChecklistId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceChecklistXRefs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceChecklistXRefs_Ambulances_AmbulanceId",
                        column: x => x.AmbulanceId,
                        principalTable: "Ambulances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmbulanceChecklistXRefs_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmbulanceChecklistXRefs_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceChecklistXRefs_AmbulanceId",
                table: "AmbulanceChecklistXRefs",
                column: "AmbulanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceChecklistXRefs_ChecklistId_AmbulanceId_TenantId",
                table: "AmbulanceChecklistXRefs",
                columns: new[] { "ChecklistId", "AmbulanceId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceChecklistXRefs_TenantId",
                table: "AmbulanceChecklistXRefs",
                column: "TenantId");
        }
    }
}
