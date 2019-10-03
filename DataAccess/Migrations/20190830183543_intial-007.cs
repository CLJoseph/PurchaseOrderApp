using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class intial007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliverToDetail",
                schema: "devDb",
                table: "PurchaseOrders",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceToDetail",
                schema: "devDb",
                table: "PurchaseOrders",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToDetail",
                schema: "devDb",
                table: "PurchaseOrders",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverToDetail",
                schema: "devDb",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "InvoiceToDetail",
                schema: "devDb",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ToDetail",
                schema: "devDb",
                table: "PurchaseOrders");
        }
    }
}
