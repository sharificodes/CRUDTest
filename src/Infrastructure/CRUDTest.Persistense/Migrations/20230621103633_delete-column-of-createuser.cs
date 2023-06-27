using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDTest.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class deletecolumnofcreateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUser",
                schema: "base",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "base",
                newName: "Products",
                newSchema: "base");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ManufacturePhone_ManufactureEmail",
                schema: "base",
                table: "Products",
                newName: "IX_Products_ManufacturePhone_ManufactureEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                schema: "base",
                newName: "Product",
                newSchema: "base");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ManufacturePhone_ManufactureEmail",
                schema: "base",
                table: "Product",
                newName: "IX_Product_ManufacturePhone_ManufactureEmail");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUser",
                schema: "base",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
