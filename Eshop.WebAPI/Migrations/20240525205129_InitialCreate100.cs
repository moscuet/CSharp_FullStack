using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "products");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "product_lines",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "product_lines");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
