using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_TblPurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems");

            migrationBuilder.RenameColumn(
                name: "TblPurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems",
                newName: "PurchaseOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderItems_TblPurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems",
                newName: "IX_PurchaseOrderItems_PurchaseOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderID",
                principalSchema: "devDb",
                principalTable: "PurchaseOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems",
                newName: "TblPurchaseOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems",
                newName: "IX_PurchaseOrderItems_TblPurchaseOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_TblPurchaseOrderID",
                schema: "devDb",
                table: "PurchaseOrderItems",
                column: "TblPurchaseOrderID",
                principalSchema: "devDb",
                principalTable: "PurchaseOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
