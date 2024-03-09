using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterDatabaseMVP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ambulances_Checklists_ChecklistId",
                table: "Ambulances");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ambulances_AmbulanceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_Name",
                table: "Checklists");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistReplacedItems_ChecklistItemId_ChecklistReviewId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AmbulanceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_ChecklistId",
                table: "Ambulances");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_Number_ChecklistId",
                table: "Ambulances");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "Ambulances");

            migrationBuilder.RenameColumn(
                name: "AmbulanceId",
                table: "AspNetUsers",
                newName: "Coren");

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Ambulances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AmbulanceChecklistXRefs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AmbulanceId = table.Column<int>(type: "integer", nullable: false),
                    ChecklistId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_Items_Name_TenantId",
                table: "Items",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_Name_TenantId",
                table: "Checklists",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistReplacedItems_ChecklistItemId_ChecklistReviewId_Te~",
                table: "ChecklistReplacedItems",
                columns: new[] { "ChecklistItemId", "ChecklistReviewId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId_TenantId",
                table: "ChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name_TenantId",
                table: "Categories",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_Number_TenantId",
                table: "Ambulances",
                columns: new[] { "Number", "TenantId" },
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmbulanceChecklistXRefs");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name_TenantId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_Name_TenantId",
                table: "Checklists");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistReplacedItems_ChecklistItemId_ChecklistReviewId_Te~",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId_TenantId",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name_TenantId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_Number_TenantId",
                table: "Ambulances");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Ambulances");

            migrationBuilder.RenameColumn(
                name: "Coren",
                table: "AspNetUsers",
                newName: "AmbulanceId");

            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "Ambulances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_Name",
                table: "Checklists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistReplacedItems_ChecklistItemId_ChecklistReviewId",
                table: "ChecklistReplacedItems",
                columns: new[] { "ChecklistItemId", "ChecklistReviewId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId",
                table: "ChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AmbulanceId",
                table: "AspNetUsers",
                column: "AmbulanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_ChecklistId",
                table: "Ambulances",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_Number_ChecklistId",
                table: "Ambulances",
                columns: new[] { "Number", "ChecklistId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ambulances_Checklists_ChecklistId",
                table: "Ambulances",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ambulances_AmbulanceId",
                table: "AspNetUsers",
                column: "AmbulanceId",
                principalTable: "Ambulances",
                principalColumn: "Id");
        }
    }
}
