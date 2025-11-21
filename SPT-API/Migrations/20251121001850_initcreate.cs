using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_API.Migrations
{
    /// <inheritdoc />
    public partial class initcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    department = table.Column<string>(type: "TEXT", nullable: false),
                    level = table.Column<string>(type: "TEXT", nullable: false),
                    studentUserName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    studentPassword = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    School = table.Column<string>(type: "TEXT", nullable: false),
                    gpa = table.Column<string>(type: "TEXT", nullable: true),
                    cgpa = table.Column<string>(type: "TEXT", nullable: true),
                    uniqueUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTable", x => x.id);
                    table.UniqueConstraint("AK_StudentTable_uniqueUserId", x => x.uniqueUserId);
                });

            migrationBuilder.CreateTable(
                name: "CourseTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isSelected = table.Column<bool>(type: "INTEGER", nullable: false),
                    cuuid = table.Column<string>(type: "TEXT", nullable: true),
                    CourseCode = table.Column<string>(type: "TEXT", nullable: false),
                    CourseTitle = table.Column<string>(type: "TEXT", nullable: true),
                    CourseUnit = table.Column<uint>(type: "INTEGER", nullable: false),
                    Grade = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTable", x => x.id);
                    table.ForeignKey(
                        name: "FK_CourseTable_StudentTable_cuuid",
                        column: x => x.cuuid,
                        principalTable: "StudentTable",
                        principalColumn: "uniqueUserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTable_cuuid",
                table: "CourseTable",
                column: "cuuid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTable_uniqueUserId",
                table: "StudentTable",
                column: "uniqueUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTable");

            migrationBuilder.DropTable(
                name: "StudentTable");
        }
    }
}
