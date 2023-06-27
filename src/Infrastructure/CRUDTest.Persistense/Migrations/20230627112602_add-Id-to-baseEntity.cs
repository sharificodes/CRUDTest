using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDTest.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class addIdtobaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "base",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "base",
                table: "Products",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "base",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "base",
                table: "Products");
        }
    }
}
