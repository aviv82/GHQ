using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHQ.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrait : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                schema: "dbo",
                table: "Traits",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                schema: "dbo",
                table: "Traits");
        }
    }
}
