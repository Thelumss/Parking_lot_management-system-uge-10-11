using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_lot_management_system_uge_10_11.Migrations
{
    /// <inheritdoc />
    public partial class updateToLotHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "lot_Histories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "lot_Histories");
        }
    }
}
