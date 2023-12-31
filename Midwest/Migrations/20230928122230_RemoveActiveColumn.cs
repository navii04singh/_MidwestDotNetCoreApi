using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midwest.Migrations
{
    /// <inheritdoc />
    public partial class RemoveActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "tblClientMachines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "tblClientMachines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
