using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiresFullReview",
                table: "Checklists",
                newName: "RequireFullReview");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequireFullReview",
                table: "Checklists",
                newName: "RequiresFullReview");
        }
    }
}
