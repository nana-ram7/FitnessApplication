using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "FitnessGoal", "Gender", "HeightFeet", "HeightInches", "IsAdmin", "LastName", "Password", "ProfilePicture", "Weight" },
                values: new object[] { 1, "admin@fitnessapp.com", "Admin", null, null, null, null, true, "User", "Admin123!", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
