﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoUrlToWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Workouts");
        }
    }
}
