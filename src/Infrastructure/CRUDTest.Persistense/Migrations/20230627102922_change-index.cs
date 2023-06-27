using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDTest.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class changeindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ManufacturePhone_ManufactureEmail",
                schema: "base",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProduceDate_ManufactureEmail",
                schema: "base",
                table: "Products",
                columns: new[] { "ProduceDate", "ManufactureEmail" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProduceDate_ManufactureEmail",
                schema: "base",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturePhone_ManufactureEmail",
                schema: "base",
                table: "Products",
                columns: new[] { "ManufacturePhone", "ManufactureEmail" });
        }
    }
}
