using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeProject.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoffeeName = table.Column<string>(type: "TEXT", nullable: false),
                    SyrupName = table.Column<string>(type: "TEXT", nullable: false),
                    HasMilk = table.Column<bool>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
