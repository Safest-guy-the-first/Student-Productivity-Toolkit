using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_Management_System_Console_App.Migrations
{
    /// <inheritdoc />
    public partial class changedresultstogrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTable_ResultTable_Result_Model_id",
                table: "CourseTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultTable_StudentTable__RuniqueUserId",
                table: "ResultTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResultTable",
                table: "ResultTable");

            migrationBuilder.RenameTable(
                name: "ResultTable",
                newName: "GradesTable");

            migrationBuilder.RenameColumn(
                name: "Result_Model_id",
                table: "CourseTable",
                newName: "Grades_Model_id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTable_Result_Model_id",
                table: "CourseTable",
                newName: "IX_CourseTable_Grades_Model_id");

            migrationBuilder.RenameIndex(
                name: "IX_ResultTable__RuniqueUserId",
                table: "GradesTable",
                newName: "IX_GradesTable__RuniqueUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradesTable",
                table: "GradesTable",
                column: "_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTable_GradesTable_Grades_Model_id",
                table: "CourseTable",
                column: "Grades_Model_id",
                principalTable: "GradesTable",
                principalColumn: "_id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradesTable_StudentTable__RuniqueUserId",
                table: "GradesTable",
                column: "_GuniqueUserId",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTable_GradesTable_Grades_Model_id",
                table: "CourseTable");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesTable_StudentTable__RuniqueUserId",
                table: "GradesTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradesTable",
                table: "GradesTable");

            migrationBuilder.RenameTable(
                name: "GradesTable",
                newName: "ResultTable");

            migrationBuilder.RenameColumn(
                name: "Grades_Model_id",
                table: "CourseTable",
                newName: "Result_Model_id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTable_Grades_Model_id",
                table: "CourseTable",
                newName: "IX_CourseTable_Result_Model_id");

            migrationBuilder.RenameIndex(
                name: "IX_GradesTable__RuniqueUserId",
                table: "ResultTable",
                newName: "IX_ResultTable__RuniqueUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResultTable",
                table: "ResultTable",
                column: "_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTable_ResultTable_Result_Model_id",
                table: "CourseTable",
                column: "Result_Model_id",
                principalTable: "ResultTable",
                principalColumn: "_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultTable_StudentTable__RuniqueUserId",
                table: "ResultTable",
                column: "_GuniqueUserId",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
