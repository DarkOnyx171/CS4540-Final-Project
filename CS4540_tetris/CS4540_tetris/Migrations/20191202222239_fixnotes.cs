using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540_tetris.Migrations
{
    public partial class fixnotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatNotes",
                columns: table => new
                {
                    noteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    note = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    gameUserId = table.Column<string>(nullable: true),
                    liked = table.Column<int>(nullable: false),
                    Time_Modified = table.Column<DateTime>(nullable: false),
                    StatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatNotes", x => x.noteID);
                    table.ForeignKey(
                        name: "FK_StatNotes_PlayerStats_StatID",
                        column: x => x.StatID,
                        principalTable: "PlayerStats",
                        principalColumn: "PlayerStatsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatNotes_GameUser_gameUserId",
                        column: x => x.gameUserId,
                        principalTable: "GameUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatNotes_StatID",
                table: "StatNotes",
                column: "StatID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatNotes_gameUserId",
                table: "StatNotes",
                column: "gameUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatNotes");
        }
    }
}
