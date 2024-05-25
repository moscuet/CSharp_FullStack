using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class eedqqa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_images_product_lines_product_line_id",
                table: "product_images");

            migrationBuilder.DropIndex(
                name: "ix_product_images_product_line_id",
                table: "product_images");

            migrationBuilder.DropColumn(
                name: "product_line_id",
                table: "product_images");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "product_lines",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "product_lines");

            migrationBuilder.AddColumn<Guid>(
                name: "product_line_id",
                table: "product_images",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_images_product_line_id",
                table: "product_images",
                column: "product_line_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_images_product_lines_product_line_id",
                table: "product_images",
                column: "product_line_id",
                principalTable: "product_lines",
                principalColumn: "id");
        }
    }
}
