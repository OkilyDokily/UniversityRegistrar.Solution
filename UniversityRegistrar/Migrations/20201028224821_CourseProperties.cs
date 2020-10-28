using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityRegistrar.Migrations
{
    public partial class CourseProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseNumber",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseNumber",
                table: "Courses");
        }
    }
}
