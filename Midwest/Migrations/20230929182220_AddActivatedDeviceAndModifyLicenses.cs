using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midwest.Migrations
{
    /// <inheritdoc />
    public partial class AddActivatedDeviceAndModifyLicenses : Migration
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

            migrationBuilder.CreateTable(
                name: "ActivatedDevice",
                columns: table => new
                {
                    ActivatedDeviceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseID = table.Column<int>(type: "int", nullable: false),
                    LicenseKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivatedDevice", x => x.ActivatedDeviceID);
                    table.ForeignKey(
                        name: "FK_ActivatedDevice_tblLicenses_LicenseKey",
                        column: x => x.LicenseKey,
                        principalTable: "tblLicenses",
                        principalColumn: "LicenseKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivatedDevice_LicenseKey",
                table: "ActivatedDevice",
                column: "LicenseKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivatedDevice");

            migrationBuilder.AddColumn<string>(
                name: "ActivatedDeviceID",
                table: "tblLicenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivatedDeviceName",
                table: "tblLicenses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
