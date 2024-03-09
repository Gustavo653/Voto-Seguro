using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_Name_TenantId",
                table: "Items");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ParentItemId",
                table: "Items",
                column: "ParentItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ParentItemId",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name_TenantId",
                table: "Items",
                columns: new[] { "Name", "TenantId" },
                unique: true);
        }
    }
}
