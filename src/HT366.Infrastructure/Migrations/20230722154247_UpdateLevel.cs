using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HT366.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Exams");
        }
    }
}
