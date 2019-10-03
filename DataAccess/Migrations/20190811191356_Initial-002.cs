using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_ApplicationUserId",
                schema: "devDb",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_Code",
                schema: "devDb",
                table: "PurchaseOrders");

            migrationBuilder.CreateIndex(
                name: "IX_UniquePO",
                schema: "devDb",
                table: "PurchaseOrders",
                columns: new[] { "ApplicationUserId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniquePO",
                schema: "devDb",
                table: "PurchaseOrders");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApplicationUserId",
                schema: "devDb",
                table: "PurchaseOrders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Code",
                schema: "devDb",
                table: "PurchaseOrders",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }
    }
}
