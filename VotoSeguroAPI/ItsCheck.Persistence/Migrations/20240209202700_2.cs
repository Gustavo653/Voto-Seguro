using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountReplaced",
                table: "ChecklistReplacedItems",
                newName: "RequiredQuantity");

            migrationBuilder.RenameColumn(
                name: "AmountRequired",
                table: "ChecklistItems",
                newName: "RequiredQuantity");

            migrationBuilder.AddColumn<int>(
                name: "ReplacedQuantity",
                table: "ChecklistReplacedItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReplenishmentQuantity",
                table: "ChecklistReplacedItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplacedQuantity",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropColumn(
                name: "ReplenishmentQuantity",
                table: "ChecklistReplacedItems");

            migrationBuilder.RenameColumn(
                name: "RequiredQuantity",
                table: "ChecklistReplacedItems",
                newName: "AmountReplaced");

            migrationBuilder.RenameColumn(
                name: "RequiredQuantity",
                table: "ChecklistItems",
                newName: "AmountRequired");
        }
    }
}
