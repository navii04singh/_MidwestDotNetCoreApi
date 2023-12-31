using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midwest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLicensesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_tblLicenses_tblClients_ClientId",
            //    table: "tblLicenses");

            migrationBuilder.DropTable(
                name: "tblClientMachines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblLicenses",
                table: "tblLicenses");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "tblLicenses");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "tblLicenses",
                newName: "ClientID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_tblLicenses_ClientId",
            //    table: "tblLicenses",
            //    newName: "IX_tblLicenses_ClientID");

            migrationBuilder.AddColumn<string>(
                name: "ActivatedDeviceID",
                table: "tblLicenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActivatedDeviceName",
                table: "tblLicenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblLicenses",
                table: "tblLicenses",
                column: "LicenseKey");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_tblLicenses_tblClients_ClientID",
            //    table: "tblLicenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblLicenses",
                table: "tblLicenses");

            migrationBuilder.DropColumn(
                name: "ActivatedDeviceID",
                table: "tblLicenses");

            migrationBuilder.DropColumn(
                name: "ActivatedDeviceName",
                table: "tblLicenses");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "tblLicenses",
                newName: "ClientId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_tblLicenses_ClientID",
            //    table: "tblLicenses",
            //    newName: "IX_tblLicenses_ClientId");

            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "tblLicenses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblLicenses",
                table: "tblLicenses",
                column: "LicenseId");

            migrationBuilder.CreateTable(
                name: "tblClientMachines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivatedDeviceID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivatedDeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    LicenseKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblClientMachines", x => x.ID);
                });

            
        }
    }
}
