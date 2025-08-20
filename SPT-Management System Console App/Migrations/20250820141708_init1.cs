using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_Management_System_Console_App.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentTable",
                columns: table => new
                {
                    _id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    department = table.Column<string>(type: "TEXT", nullable: false),
                    level = table.Column<string>(type: "TEXT", nullable: false),
                    _numLevel = table.Column<uint>(type: "INTEGER", nullable: false),
                    studentLogin = table.Column<string>(type: "TEXT", nullable: false),
                    uniqueUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTable", x => x._id);
                    table.UniqueConstraint("AK_StudentTable_uniqueUserId", x => x.uniqueUserId);
                });

            migrationBuilder.CreateTable(
                name: "GradesTable",
                columns: table => new
                {
                    _id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    _GuniqueUserId = table.Column<string>(type: "TEXT", nullable: false),
                    courseCode = table.Column<string>(type: "TEXT", nullable: false),
                    courseUnit = table.Column<uint>(type: "INTEGER", nullable: false),
                    grade = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesTable", x => x._id);
                    table.ForeignKey(
                        name: "FK_GradesTable_StudentTable__GuniqueUserId",
                        column: x => x._GuniqueUserId,
                        principalTable: "StudentTable",
                        principalColumn: "uniqueUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTable",
                columns: table => new
                {
                    _id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    _CuniqueUserId = table.Column<string>(type: "TEXT", nullable: false),
                    courseCode = table.Column<string>(type: "TEXT", nullable: false),
                    courseName = table.Column<string>(type: "TEXT", nullable: false),
                    courseUnit = table.Column<uint>(type: "INTEGER", nullable: false),
                    Grades_Model_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTable", x => x._id);
                    table.ForeignKey(
                        name: "FK_CourseTable_GradesTable_Grades_Model_id",
                        column: x => x.Grades_Model_id,
                        principalTable: "GradesTable",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "FK_CourseTable_StudentTable__CuniqueUserId",
                        column: x => x._CuniqueUserId,
                        principalTable: "StudentTable",
                        principalColumn: "uniqueUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTable__CuniqueUserId",
                table: "CourseTable",
                column: "_CuniqueUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTable_Grades_Model_id",
                table: "CourseTable",
                column: "Grades_Model_id");

            migrationBuilder.CreateIndex(
                name: "IX_GradesTable__GuniqueUserId",
                table: "GradesTable",
                column: "_GuniqueUserId");

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
                name: "GradesTable");

            migrationBuilder.DropTable(
                name: "StudentTable");
        }
    }
}
