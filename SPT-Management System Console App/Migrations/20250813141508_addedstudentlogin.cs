using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPT_Management_System_Console_App.Migrations
{
    /// <inheritdoc />
    public partial class addedstudentlogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "studentLogin",
                table: "StudentTable",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentLogin",
                table: "StudentTable");
        }
    }
}
