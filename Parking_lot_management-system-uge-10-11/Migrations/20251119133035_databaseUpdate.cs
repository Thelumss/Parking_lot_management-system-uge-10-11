using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_lot_management_system_uge_10_11.Migrations
{
    /// <inheritdoc />
    public partial class databaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LotName",
                table: "lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LotName",
                table: "lots");
        }
    }
}
