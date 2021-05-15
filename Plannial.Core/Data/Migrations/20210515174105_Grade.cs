using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Plannial.Core.Data.Migrations
{
    public partial class Grade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reminders");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Reminders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Reminders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GradeId",
                table: "Subjects",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Grades_GradeId",
                table: "Subjects",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Grades_GradeId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_GradeId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Reminders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Reminders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
