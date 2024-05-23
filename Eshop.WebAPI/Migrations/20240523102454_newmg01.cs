using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class newmg01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_status", "created,processing,completed,cancelled")
                .Annotation("Npgsql:Enum:sort_by", "price,rating,popularity,date")
                .Annotation("Npgsql:Enum:sort_order", "desc,asc,descending")
                .Annotation("Npgsql:Enum:token_type", "access_token,refresh_token")
                .Annotation("Npgsql:Enum:user_role", "super_admin,admin,user")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:Enum:color_value", "red,green,blue,black,white,yellow,pink,orange,purple,grey,brown,maroon,teal,navy,olive,lime,silver,gold,beige")
                .OldAnnotation("Npgsql:Enum:entity_type", "product,review")
                .OldAnnotation("Npgsql:Enum:order_status", "created,processing,completed,cancelled")
                .OldAnnotation("Npgsql:Enum:sort_by", "price,rating,popularity,date")
                .OldAnnotation("Npgsql:Enum:sort_order", "desc,asc,descending")
                .OldAnnotation("Npgsql:Enum:token_type", "access_token,refresh_token")
                .OldAnnotation("Npgsql:Enum:user_role", "super_admin,admin,user")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddColumn<Guid>(
                name: "product_line_id",
                table: "reviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "product_line_id",
                table: "product_images",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_line_id",
                table: "reviews",
                column: "product_line_id");

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

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_product_lines_product_line_id",
                table: "reviews",
                column: "product_line_id",
                principalTable: "product_lines",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_images_product_lines_product_line_id",
                table: "product_images");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_product_lines_product_line_id",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "ix_reviews_product_line_id",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "ix_product_images_product_line_id",
                table: "product_images");

            migrationBuilder.DropColumn(
                name: "product_line_id",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "product_line_id",
                table: "product_images");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:color_value", "red,green,blue,black,white,yellow,pink,orange,purple,grey,brown,maroon,teal,navy,olive,lime,silver,gold,beige")
                .Annotation("Npgsql:Enum:entity_type", "product,review")
                .Annotation("Npgsql:Enum:order_status", "created,processing,completed,cancelled")
                .Annotation("Npgsql:Enum:sort_by", "price,rating,popularity,date")
                .Annotation("Npgsql:Enum:sort_order", "desc,asc,descending")
                .Annotation("Npgsql:Enum:token_type", "access_token,refresh_token")
                .Annotation("Npgsql:Enum:user_role", "super_admin,admin,user")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:Enum:order_status", "created,processing,completed,cancelled")
                .OldAnnotation("Npgsql:Enum:sort_by", "price,rating,popularity,date")
                .OldAnnotation("Npgsql:Enum:sort_order", "desc,asc,descending")
                .OldAnnotation("Npgsql:Enum:token_type", "access_token,refresh_token")
                .OldAnnotation("Npgsql:Enum:user_role", "super_admin,admin,user")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }
    }
}
