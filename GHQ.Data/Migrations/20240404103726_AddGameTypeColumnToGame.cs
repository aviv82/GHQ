using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHQ.Data.Migrations
{
    public partial class AddGameTypeColumnToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "dbo",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "Games");
        }
    }
}
