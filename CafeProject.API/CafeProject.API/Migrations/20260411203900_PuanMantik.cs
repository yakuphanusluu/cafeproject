using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeProject.API.Migrations
{
    /// <inheritdoc />
    public partial class PuanMantik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UsedPoints",
                table: "Orders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedPoints",
                table: "Orders");
        }
    }
}
