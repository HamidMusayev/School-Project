using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Migrations
{
    public partial class ut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Class_LessonId",
                table: "Exam");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Lesson_LessonId",
                table: "Exam",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Lesson_LessonId",
                table: "Exam");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Class_LessonId",
                table: "Exam",
                column: "LessonId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
