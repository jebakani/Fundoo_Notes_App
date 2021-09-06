using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class labelfundoos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Label",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Label",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
