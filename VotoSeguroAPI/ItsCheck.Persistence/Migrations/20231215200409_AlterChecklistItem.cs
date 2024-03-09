using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterChecklistItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Items_ParentItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name_TenantId_ParentItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ParentItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId_TenantId",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_Number_TenantId",
                table: "Ambulances");

            migrationBuilder.DropColumn(
                name: "ParentItemId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ParentChecklistItemId",
                table: "ChecklistItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name_TenantId",
                table: "Items",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId_TenantId_Paren~",
                table: "ChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId", "TenantId", "ParentChecklistItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ParentChecklistItemId",
                table: "ChecklistItems",
                column: "ParentChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_Number_LicensePlate_TenantId",
                table: "Ambulances",
                columns: new[] { "Number", "LicensePlate", "TenantId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_ChecklistItems_ParentChecklistItemId",
                table: "ChecklistItems",
                column: "ParentChecklistItemId",
                principalTable: "ChecklistItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_ChecklistItems_ParentChecklistItemId",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name_TenantId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId_TenantId_Paren~",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_ParentChecklistItemId",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_Number_LicensePlate_TenantId",
                table: "Ambulances");

            migrationBuilder.DropColumn(
                name: "ParentChecklistItemId",
                table: "ChecklistItems");

            migrationBuilder.AddColumn<int>(
                name: "ParentItemId",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name_TenantId_ParentItemId",
                table: "Items",
                columns: new[] { "Name", "TenantId", "ParentItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ParentItemId",
                table: "Items",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId_TenantId",
                table: "ChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_Number_TenantId",
                table: "Ambulances",
                columns: new[] { "Number", "TenantId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ParentItemId",
                table: "Items",
                column: "ParentItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
