using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_Management_System_Console_App.Migrations
{
    /// <inheritdoc />
    public partial class AddedResultTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_CuniqueUserId",
                table: "ResultTable",
                newName: "_uniqueUserId");

            migrationBuilder.AddColumn<int>(
                name: "Result_Model_id",
                table: "CourseTable",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultTable__uniqueUserId",
                table: "ResultTable",
                column: "_uniqueUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTable_Result_Model_id",
                table: "CourseTable",
                column: "Result_Model_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTable_ResultTable_Result_Model_id",
                table: "CourseTable",
                column: "Result_Model_id",
                principalTable: "ResultTable",
                principalColumn: "_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultTable_StudentTable__uniqueUserId",
                table: "ResultTable",
                column: "_uniqueUserId",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTable_ResultTable_Result_Model_id",
                table: "CourseTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultTable_StudentTable__uniqueUserId",
                table: "ResultTable");

            migrationBuilder.DropIndex(
                name: "IX_ResultTable__uniqueUserId",
                table: "ResultTable");

            migrationBuilder.DropIndex(
                name: "IX_CourseTable_Result_Model_id",
                table: "CourseTable");

            migrationBuilder.DropColumn(
                name: "Result_Model_id",
                table: "CourseTable");

            migrationBuilder.RenameColumn(
                name: "_uniqueUserId",
                table: "ResultTable",
                newName: "_CuniqueUserId");
        }
    }
}
