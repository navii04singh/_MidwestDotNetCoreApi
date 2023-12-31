using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midwest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActivatedDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicenseID",
                table: "ActivatedDevice",
                newName: "ClientID");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ActivatedDevice",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ActivatedDevice");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "ActivatedDevice",
                newName: "LicenseID");
        }
    }
}
