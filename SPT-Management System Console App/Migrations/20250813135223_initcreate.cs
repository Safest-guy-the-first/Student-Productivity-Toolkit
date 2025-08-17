using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_Management_System_Console_App.Migrations
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
                    _id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    course = table.Column<string>(type: "TEXT", nullable: false),
                    level = table.Column<string>(type: "TEXT", nullable: false),
                    _numLevel = table.Column<uint>(type: "INTEGER", nullable: false),
                    uniqueUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTable", x => x._id);
                    table.UniqueConstraint("AK_StudentTable_uniqueUserId", x => x.uniqueUserId);
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
                    courseUnit = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTable", x => x._id);
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
