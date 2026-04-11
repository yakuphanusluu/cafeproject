using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeProject.API.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciSistemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerPhone",
                table: "Orders",
                newName: "CustomerUsername");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerUsername",
                table: "Orders",
                newName: "CustomerPhone");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Customers",
                newName: "Name");
        }
    }
}
