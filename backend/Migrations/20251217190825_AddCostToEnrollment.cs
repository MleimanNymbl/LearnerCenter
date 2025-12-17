using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnerCenter.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCostToEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Enrollments",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 2500.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Enrollments");
        }
    }
}
