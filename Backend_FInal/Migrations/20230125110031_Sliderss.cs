using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendFinal.Migrations
{
    /// <inheritdoc />
    public partial class Sliderss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backgroundİmage",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "BackgroundİmageInSystem",
                table: "Sliders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Backgroundİmage",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundİmageInSystem",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
