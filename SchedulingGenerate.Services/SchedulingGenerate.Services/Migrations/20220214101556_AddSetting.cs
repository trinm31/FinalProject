using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingGenerate.Services.Migrations
{
    public partial class AddSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyLevelDefault = table.Column<int>(type: "int", nullable: false),
                    InternalDistance = table.Column<int>(type: "int", nullable: false),
                    ExternalDistance = table.Column<int>(type: "int", nullable: false),
                    NoOfTimeSlot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
