using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Services.Migrations
{
    public partial class AddmoreUserInfor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersionalId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersionalId",
                table: "AspNetUsers");
        }
    }
}
