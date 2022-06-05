using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Migrations
{
    public partial class ud2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Class_ClassId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Class_ClassId",
                table: "User",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Class_ClassId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Class_ClassId",
                table: "User",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");
        }
    }
}
