using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParkingApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    LicensePlate = table.Column<string>(type: "TEXT", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HourlyRate = table.Column<double>(type: "REAL", nullable: false),
                    VehicleType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.LicensePlate);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
