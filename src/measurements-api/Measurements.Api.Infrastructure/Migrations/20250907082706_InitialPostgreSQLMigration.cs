using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Measurements.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSQLMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    Pressure = table.Column<double>(type: "double precision", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    Battery = table.Column<double>(type: "double precision", nullable: false),
                    Acceleration_Acceleration1 = table.Column<double>(type: "double precision", nullable: true),
                    Acceleration_AccelerationX = table.Column<double>(type: "double precision", nullable: true),
                    Acceleration_AccelerationY = table.Column<double>(type: "double precision", nullable: true),
                    Acceleration_AccelerationZ = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_Source_Time",
                table: "Measurements",
                columns: new[] { "Source", "Time" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Sensors");
        }
    }
}
