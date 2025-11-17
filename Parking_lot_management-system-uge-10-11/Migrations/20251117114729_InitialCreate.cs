using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_lot_management_system_uge_10_11.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lot_Types",
                columns: table => new
                {
                    Lot_typesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price_Multiplier = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lot_Types", x => x.Lot_typesID);
                });

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    OrganisationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.OrganisationID);
                });

            migrationBuilder.CreateTable(
                name: "user_Types",
                columns: table => new
                {
                    User_TypesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_TypesName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Types", x => x.User_TypesID);
                });

            migrationBuilder.CreateTable(
                name: "parking_Lot_Structurs",
                columns: table => new
                {
                    Parking_lot_Structur_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Available_Lots = table.Column<int>(type: "int", nullable: false),
                    Total_Occupied_Lots = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<float>(type: "real", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parking_Lot_Structurs", x => x.Parking_lot_Structur_ID);
                    table.ForeignKey(
                        name: "FK_parking_Lot_Structurs_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "OrganisationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypeID = table.Column<int>(type: "int", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "OrganisationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_user_Types_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "user_Types",
                        principalColumn: "User_TypesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lots",
                columns: table => new
                {
                    LotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occupied_Status = table.Column<bool>(type: "bit", nullable: false),
                    Structur_ID = table.Column<int>(type: "int", nullable: false),
                    Lot_types_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lots", x => x.LotID);
                    table.ForeignKey(
                        name: "FK_lots_lot_Types_Lot_types_ID",
                        column: x => x.Lot_types_ID,
                        principalTable: "lot_Types",
                        principalColumn: "Lot_typesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lots_parking_Lot_Structurs_Structur_ID",
                        column: x => x.Structur_ID,
                        principalTable: "parking_Lot_Structurs",
                        principalColumn: "Parking_lot_Structur_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lot_Histories",
                columns: table => new
                {
                    Lot_History_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    License_PLate_Numbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entry_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Exit_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Charged = table.Column<float>(type: "real", nullable: false),
                    Lot_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lot_Histories", x => x.Lot_History_ID);
                    table.ForeignKey(
                        name: "FK_lot_Histories_lots_Lot_ID",
                        column: x => x.Lot_ID,
                        principalTable: "lots",
                        principalColumn: "LotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lot_Histories_Lot_ID",
                table: "lot_Histories",
                column: "Lot_ID");

            migrationBuilder.CreateIndex(
                name: "IX_lots_Lot_types_ID",
                table: "lots",
                column: "Lot_types_ID");

            migrationBuilder.CreateIndex(
                name: "IX_lots_Structur_ID",
                table: "lots",
                column: "Structur_ID");

            migrationBuilder.CreateIndex(
                name: "IX_parking_Lot_Structurs_OrganisationId",
                table: "parking_Lot_Structurs",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganisationId",
                table: "Users",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeID",
                table: "Users",
                column: "UserTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lot_Histories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "lots");

            migrationBuilder.DropTable(
                name: "user_Types");

            migrationBuilder.DropTable(
                name: "lot_Types");

            migrationBuilder.DropTable(
                name: "parking_Lot_Structurs");

            migrationBuilder.DropTable(
                name: "Organisation");
        }
    }
}
