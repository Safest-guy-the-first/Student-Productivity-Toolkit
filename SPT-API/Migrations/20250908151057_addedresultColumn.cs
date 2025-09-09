using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_API.Migrations
{
    /// <inheritdoc />
    public partial class addedresultColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTable_StudentTable_cuuid",
                table: "CourseTable");

            migrationBuilder.AlterColumn<string>(
                name: "cuuid",
                table: "CourseTable",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<char>(
                name: "Grade",
                table: "CourseTable",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTable_StudentTable_cuuid",
                table: "CourseTable",
                column: "cuuid",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTable_StudentTable_cuuid",
                table: "CourseTable");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "CourseTable");

            migrationBuilder.AlterColumn<string>(
                name: "cuuid",
                table: "CourseTable",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTable_StudentTable_cuuid",
                table: "CourseTable",
                column: "cuuid",
                principalTable: "StudentTable",
                principalColumn: "uniqueUserId");
        }
    }
}
