using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApplication.Migrations
{
    /// <inheritdoc />
    public partial class fixerrror : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Workouts",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "WorkoutId", "Category", "Description", "Duration", "ThumbnailUrl", "VideoUrl", "WorkoutName" },
                values: new object[] { 2, "Yoga", "Strengthen and release the low back.", 32, null, "https://www.youtube.com/watch?v=XYZ123", "Gentle Yoga For Alignment" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "WorkoutId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Workouts",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Workouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
