using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationItems_TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems");

            migrationBuilder.RenameColumn(
                name: "TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems",
                newName: "TblOrganisationId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TblOrganisationId",
                schema: "devDb",
                table: "OrganisationItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniqueItem",
                schema: "devDb",
                table: "OrganisationItems",
                columns: new[] { "TblOrganisationId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationId",
                schema: "devDb",
                table: "OrganisationItems",
                column: "TblOrganisationId",
                principalSchema: "devDb",
                principalTable: "Organisations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationId",
                schema: "devDb",
                table: "OrganisationItems");

            migrationBuilder.DropIndex(
                name: "IX_UniqueItem",
                schema: "devDb",
                table: "OrganisationItems");

            migrationBuilder.RenameColumn(
                name: "TblOrganisationId",
                schema: "devDb",
                table: "OrganisationItems",
                newName: "TblOrganisationID");

            migrationBuilder.AlterColumn<Guid>(
                name: "TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationItems_TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems",
                column: "TblOrganisationID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems",
                column: "TblOrganisationID",
                principalSchema: "devDb",
                principalTable: "Organisations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
