using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeProject.API.Migrations
{
    /// <inheritdoc />
    public partial class EkstralarEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extras",
                table: "Orders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extras",
                table: "Orders");
        }
    }
}
