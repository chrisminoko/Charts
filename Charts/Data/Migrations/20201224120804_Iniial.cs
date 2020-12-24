using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Charts.Data.Migrations
{
    public partial class Iniial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "salesRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    Electronics = table.Column<int>(nullable: false),
                    BookAndMedia = table.Column<int>(nullable: false),
                    HomeAndKitchen = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salesRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "salesRecords");
        }
    }
}
