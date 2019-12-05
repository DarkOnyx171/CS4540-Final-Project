using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS4540_tetris.Migrations
{
    public partial class updatedstatstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatNotes_GameUser_gameUserId",
                table: "StatNotes");

            migrationBuilder.DropIndex(
                name: "IX_StatNotes_gameUserId",
                table: "StatNotes");

            migrationBuilder.DropColumn(
                name: "Time_Modified",
                table: "StatNotes");

            migrationBuilder.DropColumn(
                name: "gameUserId",
                table: "StatNotes");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "StatNotes",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "StatNotes",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "liked",
                table: "StatNotes",
                newName: "Liked");

            migrationBuilder.RenameColumn(
                name: "noteID",
                table: "StatNotes",
                newName: "NoteID");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeModified",
                table: "StatNotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeModified",
                table: "StatNotes");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "StatNotes",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "StatNotes",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "Liked",
                table: "StatNotes",
                newName: "liked");

            migrationBuilder.RenameColumn(
                name: "NoteID",
                table: "StatNotes",
                newName: "noteID");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time_Modified",
                table: "StatNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "gameUserId",
                table: "StatNotes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatNotes_gameUserId",
                table: "StatNotes",
                column: "gameUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatNotes_GameUser_gameUserId",
                table: "StatNotes",
                column: "gameUserId",
                principalTable: "GameUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
