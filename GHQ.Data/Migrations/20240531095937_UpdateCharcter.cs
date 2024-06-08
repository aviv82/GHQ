using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHQ.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCharcter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerCharacters",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                schema: "dbo",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlayerId",
                schema: "dbo",
                table: "Characters",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Players_PlayerId",
                schema: "dbo",
                table: "Characters",
                column: "PlayerId",
                principalSchema: "dbo",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Players_PlayerId",
                schema: "dbo",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PlayerId",
                schema: "dbo",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                schema: "dbo",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "PlayerCharacters",
                schema: "dbo",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCharacters", x => new { x.CharacterId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "dbo",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "dbo",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_CharacterId",
                schema: "dbo",
                table: "PlayerCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_PlayerId",
                schema: "dbo",
                table: "PlayerCharacters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_PlayerId_CharacterId",
                schema: "dbo",
                table: "PlayerCharacters",
                columns: new[] { "PlayerId", "CharacterId" },
                unique: true);
        }
    }
}
