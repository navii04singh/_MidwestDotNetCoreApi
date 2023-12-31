using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midwest.Migrations
{
    /// <inheritdoc />
    public partial class AddLicensesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "tblClients");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseKey",
                table: "tblClients",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "tblLicenses",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLicenses", x => x.LicenseId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblLicenses");

            migrationBuilder.AlterColumn<string>(
                name: "LicenseKey",
                table: "tblClients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "tblClients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
