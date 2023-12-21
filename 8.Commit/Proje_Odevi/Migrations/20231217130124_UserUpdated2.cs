using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Odevi.Migrations
{
    public partial class UserUpdated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageFileName",
                table: "Kullanıcılar",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "noimage.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageFileName",
                table: "Kullanıcılar");
        }
    }
}
