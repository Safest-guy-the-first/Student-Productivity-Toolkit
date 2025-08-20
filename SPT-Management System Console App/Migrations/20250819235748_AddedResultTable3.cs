using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_Management_System_Console_App.Migrations
{
    /// <inheritdoc />
    public partial class AddedResultTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultTable_StudentTable__uniqueUserId",
                table: "ResultTable");

            migrationBuilder.RenameColumn(
                name: "_uniqueUserId",
                table: "ResultTable",
                newName: "_GuniqueUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultTable__uniqueUserId",
                table: "ResultTable",
                newName: "IX_ResultTable__RuniqueUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultTable_StudentTable__RuniqueUserId",
                table: "ResultTable",
                column: "_GuniqueUserId",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultTable_StudentTable__RuniqueUserId",
                table: "ResultTable");

            migrationBuilder.RenameColumn(
                name: "_GuniqueUserId",
                table: "ResultTable",
                newName: "_uniqueUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultTable__RuniqueUserId",
                table: "ResultTable",
                newName: "IX_ResultTable__uniqueUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultTable_StudentTable__uniqueUserId",
                table: "ResultTable",
                column: "_uniqueUserId",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
