using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Checklists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ChecklistReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ChecklistReplacedItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ChecklistItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Ambulances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_TenantId",
                table: "Items",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_TenantId",
                table: "Checklists",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistReviews_TenantId",
                table: "ChecklistReviews",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistReplacedItems_TenantId",
                table: "ChecklistReplacedItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_TenantId",
                table: "ChecklistItems",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TenantId",
                table: "Categories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_TenantId",
                table: "Ambulances",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ambulances_Tenant_TenantId",
                table: "Ambulances",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Tenant_TenantId",
                table: "Categories",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_Tenant_TenantId",
                table: "ChecklistItems",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReplacedItems_Tenant_TenantId",
                table: "ChecklistReplacedItems",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReviews_Tenant_TenantId",
                table: "ChecklistReviews",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_Tenant_TenantId",
                table: "Checklists",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Tenant_TenantId",
                table: "Items",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ambulances_Tenant_TenantId",
                table: "Ambulances");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenant_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Tenant_TenantId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_Tenant_TenantId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReplacedItems_Tenant_TenantId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReviews_Tenant_TenantId",
                table: "ChecklistReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_Tenant_TenantId",
                table: "Checklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Tenant_TenantId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Items_TenantId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_TenantId",
                table: "Checklists");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistReviews_TenantId",
                table: "ChecklistReviews");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistReplacedItems_TenantId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_TenantId",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TenantId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_TenantId",
                table: "Ambulances");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ChecklistReviews");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ChecklistItems");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Ambulances");
        }
    }
}
