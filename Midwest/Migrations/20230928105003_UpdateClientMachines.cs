using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midwest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientMachines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivatedDeviceID",
                table: "tblLicenses");

            migrationBuilder.DropColumn(
                name: "ActivatedDeviceName",
                table: "tblLicenses");

            migrationBuilder.RenameColumn(
                name: "MachineID",
                table: "tblClientMachines",
                newName: "ActivatedDeviceName");

            migrationBuilder.AddColumn<string>(
                name: "ActivatedDeviceID",
                table: "tblClientMachines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseKey",
                table: "tblClientMachines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivatedDeviceID",
                table: "tblClientMachines");

            migrationBuilder.DropColumn(
                name: "LicenseKey",
                table: "tblClientMachines");

            migrationBuilder.RenameColumn(
                name: "ActivatedDeviceName",
                table: "tblClientMachines",
                newName: "MachineID");

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
        }
    }
}
