using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "devDb",
                table: "Organisations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                schema: "devDb",
                table: "Organisations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UniqueOrg",
                schema: "devDb",
                table: "Organisations",
                columns: new[] { "ApplicationUserId", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_AspNetUsers_ApplicationUserId",
                schema: "devDb",
                table: "Organisations",
                column: "ApplicationUserId",
                principalSchema: "devDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_AspNetUsers_ApplicationUserId",
                schema: "devDb",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_UniqueOrg",
                schema: "devDb",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "devDb",
                table: "Organisations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "devDb",
                table: "Organisations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
