using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_API.Migrations
{
    /// <inheritdoc />
    public partial class Alotwasdone2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Grade",
                table: "CourseTable",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(char),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Grade",
                table: "CourseTable",
                type: "TEXT",
                nullable: false,
                defaultValue: '\0',
                oldClrType: typeof(char),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
