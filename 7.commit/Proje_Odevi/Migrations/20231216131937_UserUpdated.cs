using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Odevi.Migrations
{
    public partial class UserUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Kullanıcılar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Kullanıcılar");
        }
    }
}
