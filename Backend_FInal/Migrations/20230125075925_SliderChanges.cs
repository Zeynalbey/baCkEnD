using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendFinal.Migrations
{
    /// <inheritdoc />
    public partial class SliderChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BackgroundİmageInFileSystem",
                table: "Sliders",
                newName: "İmageInSystem");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundİmageInSystem",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "İmage",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundİmageInSystem",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "İmage",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "İmageInSystem",
                table: "Sliders",
                newName: "BackgroundİmageInFileSystem");
        }
    }
}
