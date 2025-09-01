using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentTable_uniqueUserId",
                table: "StudentTable",
                column: "uniqueUserId");

            migrationBuilder.CreateTable(
                name: "CourseTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cuuid = table.Column<string>(type: "TEXT", nullable: true),
                    CourseCode = table.Column<string>(type: "TEXT", nullable: true),
                    CourseTitle = table.Column<string>(type: "TEXT", nullable: true),
                    CourseUnit = table.Column<uint>(type: "INTEGER", nullable: true)
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
                name: "IX_StudentTable_uniqueUserId",
                table: "StudentTable",
                column: "uniqueUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTable_cuuid",
                table: "CourseTable",
                column: "cuuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTable");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentTable_uniqueUserId",
                table: "StudentTable");

            migrationBuilder.DropIndex(
                name: "IX_StudentTable_uniqueUserId",
                table: "StudentTable");
        }
    }
}
