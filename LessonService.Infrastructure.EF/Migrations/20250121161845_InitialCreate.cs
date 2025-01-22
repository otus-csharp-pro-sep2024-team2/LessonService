using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonService.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lessonsrv");

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "lessonsrv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                schema: "lessonsrv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "lessonsrv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false, defaultValue: 60),
                    TrainingLevel = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    LessonType = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    MaxStudents = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    LessonStatus = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalSchema: "lessonsrv",
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LessonGroups",
                schema: "lessonsrv",
                columns: table => new
                {
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroups", x => new { x.LessonId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_LessonGroups_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "lessonsrv",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonGroupStudents",
                schema: "lessonsrv",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonGroupsLessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonGroupsStudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroupStudents", x => new { x.StudentsId, x.LessonGroupsLessonId, x.LessonGroupsStudentId });
                    table.ForeignKey(
                        name: "FK_LessonGroupStudents_LessonGroups_LessonGroupsLessonId_Lesso~",
                        columns: x => new { x.LessonGroupsLessonId, x.LessonGroupsStudentId },
                        principalSchema: "lessonsrv",
                        principalTable: "LessonGroups",
                        principalColumns: new[] { "LessonId", "StudentId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonGroupStudents_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalSchema: "lessonsrv",
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroupStudents_LessonGroupsLessonId_LessonGroupsStuden~",
                schema: "lessonsrv",
                table: "LessonGroupStudents",
                columns: new[] { "LessonGroupsLessonId", "LessonGroupsStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TrainerId",
                schema: "lessonsrv",
                table: "Lessons",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonGroupStudents",
                schema: "lessonsrv");

            migrationBuilder.DropTable(
                name: "LessonGroups",
                schema: "lessonsrv");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "lessonsrv");

            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "lessonsrv");

            migrationBuilder.DropTable(
                name: "Trainers",
                schema: "lessonsrv");
        }
    }
}
