using Microsoft.EntityFrameworkCore.Migrations;

namespace Plannial.Core.Data.Migrations
{
    public partial class TwoAggregatesHomeworkAndExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Subjects_SubjectId",
                table: "Homeworks");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Homeworks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Subjects_SubjectId",
                table: "Homeworks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Subjects_SubjectId",
                table: "Homeworks");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Homeworks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Exams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Subjects_SubjectId",
                table: "Homeworks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
