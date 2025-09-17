using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_API.Migrations
{
    /// <inheritdoc />
    public partial class Alotwasdone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "studentUserName",
                table: "StudentTable",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "studentPassword",
                table: "StudentTable",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "StudentTable",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<char>(
                name: "Grade",
                table: "CourseTable",
                type: "TEXT",
                nullable: false,
                defaultValue: 'F',
                oldClrType: typeof(char),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "CourseUnit",
                table: "CourseTable",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "School",
                table: "StudentTable");

            migrationBuilder.AlterColumn<string>(
                name: "studentUserName",
                table: "StudentTable",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "studentPassword",
                table: "StudentTable",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<char>(
                name: "Grade",
                table: "CourseTable",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(char),
                defaultValue: "F",
                oldType: "TEXT");

            migrationBuilder.AlterColumn<uint>(
                name: "CourseUnit",
                table: "CourseTable",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");
        }
    }
}
