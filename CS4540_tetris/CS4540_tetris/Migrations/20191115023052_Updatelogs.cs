using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540_tetris.Migrations
{
    public partial class Updatelogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultiPlayerLogs",
                columns: table => new
                {
                    MultiPlayerLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameOneID = table.Column<int>(nullable: false),
                    GameLogOneGameID = table.Column<int>(nullable: true),
                    GameTwoID = table.Column<int>(nullable: false),
                    GameLogTwoGameID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiPlayerLogs", x => x.MultiPlayerLogID);
                    table.ForeignKey(
                        name: "FK_MultiPlayerLogs_GameLogs_GameLogOneGameID",
                        column: x => x.GameLogOneGameID,
                        principalTable: "GameLogs",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultiPlayerLogs_GameLogs_GameLogTwoGameID",
                        column: x => x.GameLogTwoGameID,
                        principalTable: "GameLogs",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiPlayerLogs_GameLogOneGameID",
                table: "MultiPlayerLogs",
                column: "GameLogOneGameID");

            migrationBuilder.CreateIndex(
                name: "IX_MultiPlayerLogs_GameLogTwoGameID",
                table: "MultiPlayerLogs",
                column: "GameLogTwoGameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultiPlayerLogs");

            migrationBuilder.AddColumn<int>(
                name: "MultiPlayerLogID",
                table: "GameLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameLogs_MultiPlayerLogID",
                table: "GameLogs",
                column: "MultiPlayerLogID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameLogs_GameLogs_MultiPlayerLogID",
                table: "GameLogs",
                column: "MultiPlayerLogID",
                principalTable: "GameLogs",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
