using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixThumnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workouts",
                keyColumn: "WorkoutId",
                keyValue: 2,
                columns: new[] { "Duration", "VideoUrl", "WorkoutName" },
                values: new object[] { 15, "https://www.youtube.com/watch?v=EvMTrP8eRvM", "15 min Gentle Yoga for Flexibility & Stress Reduction" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workouts",
                keyColumn: "WorkoutId",
                keyValue: 2,
                columns: new[] { "Duration", "VideoUrl", "WorkoutName" },
                values: new object[] { 32, "https://www.youtube.com/watch?v=XYZ123", "Gentle Yoga For Alignment" });
        }
    }
}
