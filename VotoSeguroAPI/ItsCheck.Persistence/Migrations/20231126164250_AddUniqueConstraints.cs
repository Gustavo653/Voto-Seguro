using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsCheck.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ambulance_Checklist_ChecklistId",
                table: "Ambulance");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ambulance_AmbulanceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Category_CategoryId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Checklist_ChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Item_ItemId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReplacedItem_ChecklistItem_ChecklistItemId",
                table: "ChecklistReplacedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReplacedItem_ChecklistReview_ChecklistReviewId",
                table: "ChecklistReplacedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReview_Ambulance_AmbulanceId",
                table: "ChecklistReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReview_AspNetUsers_UserId",
                table: "ChecklistReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReview_Checklist_ChecklistId",
                table: "ChecklistReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistReview",
                table: "ChecklistReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistReplacedItem",
                table: "ChecklistReplacedItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistReplacedItem_ChecklistItemId",
                table: "ChecklistReplacedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItem",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItem_ItemId",
                table: "ChecklistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checklist",
                table: "Checklist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ambulance",
                table: "Ambulance");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "ChecklistReview",
                newName: "ChecklistReviews");

            migrationBuilder.RenameTable(
                name: "ChecklistReplacedItem",
                newName: "ChecklistReplacedItems");

            migrationBuilder.RenameTable(
                name: "ChecklistItem",
                newName: "ChecklistItems");

            migrationBuilder.RenameTable(
                name: "Checklist",
                newName: "Checklists");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Ambulance",
                newName: "Ambulances");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReview_UserId",
                table: "ChecklistReviews",
                newName: "IX_ChecklistReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReview_ChecklistId",
                table: "ChecklistReviews",
                newName: "IX_ChecklistReviews_ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReview_AmbulanceId",
                table: "ChecklistReviews",
                newName: "IX_ChecklistReviews_AmbulanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReplacedItem_ChecklistReviewId",
                table: "ChecklistReplacedItems",
                newName: "IX_ChecklistReplacedItems_ChecklistReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItems",
                newName: "IX_ChecklistItems_ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItem_CategoryId",
                table: "ChecklistItems",
                newName: "IX_ChecklistItems_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ambulance_ChecklistId",
                table: "Ambulances",
                newName: "IX_Ambulances_ChecklistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistReviews",
                table: "ChecklistReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistReplacedItems",
                table: "ChecklistReplacedItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItems",
                table: "ChecklistItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checklists",
                table: "Checklists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ambulances",
                table: "Ambulances",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
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
                name: "IX_Checklists_Name",
                table: "Checklists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_Categories_CategoryId",
                table: "ChecklistItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_Checklists_ChecklistId",
                table: "ChecklistItems",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_Items_ItemId",
                table: "ChecklistItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReplacedItems_ChecklistItems_ChecklistItemId",
                table: "ChecklistReplacedItems",
                column: "ChecklistItemId",
                principalTable: "ChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReplacedItems_ChecklistReviews_ChecklistReviewId",
                table: "ChecklistReplacedItems",
                column: "ChecklistReviewId",
                principalTable: "ChecklistReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReviews_Ambulances_AmbulanceId",
                table: "ChecklistReviews",
                column: "AmbulanceId",
                principalTable: "Ambulances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReviews_AspNetUsers_UserId",
                table: "ChecklistReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReviews_Checklists_ChecklistId",
                table: "ChecklistReviews",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ambulances_Checklists_ChecklistId",
                table: "Ambulances");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ambulances_AmbulanceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_Categories_CategoryId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_Checklists_ChecklistId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_Items_ItemId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReplacedItems_ChecklistItems_ChecklistItemId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReplacedItems_ChecklistReviews_ChecklistReviewId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReviews_Ambulances_AmbulanceId",
                table: "ChecklistReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReviews_AspNetUsers_UserId",
                table: "ChecklistReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistReviews_Checklists_ChecklistId",
                table: "ChecklistReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Name",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checklists",
                table: "Checklists");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_Name",
                table: "Checklists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistReviews",
                table: "ChecklistReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistReplacedItems",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistReplacedItems_ChecklistItemId_ChecklistReviewId",
                table: "ChecklistReplacedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItems",
                table: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItems_ItemId_CategoryId_ChecklistId",
                table: "ChecklistItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ambulances",
                table: "Ambulances");

            migrationBuilder.DropIndex(
                name: "IX_Ambulances_Number_ChecklistId",
                table: "Ambulances");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "Checklists",
                newName: "Checklist");

            migrationBuilder.RenameTable(
                name: "ChecklistReviews",
                newName: "ChecklistReview");

            migrationBuilder.RenameTable(
                name: "ChecklistReplacedItems",
                newName: "ChecklistReplacedItem");

            migrationBuilder.RenameTable(
                name: "ChecklistItems",
                newName: "ChecklistItem");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Ambulances",
                newName: "Ambulance");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReviews_UserId",
                table: "ChecklistReview",
                newName: "IX_ChecklistReview_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReviews_ChecklistId",
                table: "ChecklistReview",
                newName: "IX_ChecklistReview_ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReviews_AmbulanceId",
                table: "ChecklistReview",
                newName: "IX_ChecklistReview_AmbulanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistReplacedItems_ChecklistReviewId",
                table: "ChecklistReplacedItem",
                newName: "IX_ChecklistReplacedItem_ChecklistReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItems_ChecklistId",
                table: "ChecklistItem",
                newName: "IX_ChecklistItem_ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItems_CategoryId",
                table: "ChecklistItem",
                newName: "IX_ChecklistItem_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ambulances_ChecklistId",
                table: "Ambulance",
                newName: "IX_Ambulance_ChecklistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checklist",
                table: "Checklist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistReview",
                table: "ChecklistReview",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistReplacedItem",
                table: "ChecklistReplacedItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItem",
                table: "ChecklistItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ambulance",
                table: "Ambulance",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistReplacedItem_ChecklistItemId",
                table: "ChecklistReplacedItem",
                column: "ChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_ItemId",
                table: "ChecklistItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ambulance_Checklist_ChecklistId",
                table: "Ambulance",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ambulance_AmbulanceId",
                table: "AspNetUsers",
                column: "AmbulanceId",
                principalTable: "Ambulance",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Category_CategoryId",
                table: "ChecklistItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Checklist_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Item_ItemId",
                table: "ChecklistItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReplacedItem_ChecklistItem_ChecklistItemId",
                table: "ChecklistReplacedItem",
                column: "ChecklistItemId",
                principalTable: "ChecklistItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReplacedItem_ChecklistReview_ChecklistReviewId",
                table: "ChecklistReplacedItem",
                column: "ChecklistReviewId",
                principalTable: "ChecklistReview",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReview_Ambulance_AmbulanceId",
                table: "ChecklistReview",
                column: "AmbulanceId",
                principalTable: "Ambulance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReview_AspNetUsers_UserId",
                table: "ChecklistReview",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistReview_Checklist_ChecklistId",
                table: "ChecklistReview",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
