using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Odevi.Migrations
{
    public partial class PolyclinicsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poliklinik_Ekleme",
                columns: table => new
                {
                    PolyclinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolyclinicName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliklinik_Ekleme", x => x.PolyclinicId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poliklinik_Ekleme");
        }
    }
}
