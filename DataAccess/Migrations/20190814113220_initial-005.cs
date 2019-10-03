using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationsID",
                schema: "devDb",
                table: "OrganisationItems");

            migrationBuilder.RenameColumn(
                name: "TblOrganisationsID",
                schema: "devDb",
                table: "OrganisationItems",
                newName: "TblOrganisationID");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationItems_TblOrganisationsID",
                schema: "devDb",
                table: "OrganisationItems",
                newName: "IX_OrganisationItems_TblOrganisationID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "devDb",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "devDb",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                schema: "devDb",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "devDb",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "devDb",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems");

            migrationBuilder.DropColumn(
                name: "PersonName",
                schema: "devDb",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems",
                newName: "TblOrganisationsID");

            migrationBuilder.RenameIndex(
                name: "IX_OrganisationItems_TblOrganisationID",
                schema: "devDb",
                table: "OrganisationItems",
                newName: "IX_OrganisationItems_TblOrganisationsID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "devDb",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "devDb",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "devDb",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "devDb",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationItems_Organisations_TblOrganisationsID",
                schema: "devDb",
                table: "OrganisationItems",
                column: "TblOrganisationsID",
                principalSchema: "devDb",
                principalTable: "Organisations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
