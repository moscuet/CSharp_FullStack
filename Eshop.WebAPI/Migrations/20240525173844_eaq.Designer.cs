﻿// <auto-generated />
using System;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eshop.WebAPI.Migrations
{
    [DbContext(typeof(EshopDbContext))]
    [Migration("20240525173844_eaq")]
    partial class eaq
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "order_status", new[] { "created", "processing", "completed", "cancelled" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "sort_by", new[] { "price", "rating", "popularity", "date" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "sort_order", new[] { "desc", "asc", "descending" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "token_type", new[] { "access_token", "refresh_token" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_role", new[] { "super_admin", "admin", "user" });
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Eshop.Core.src.Entity.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("country");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("house");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("street");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("zip_code");

                    b.HasKey("Id")
                        .HasName("pk_addresses");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_addresses_user_id");

                    b.ToTable("addresses", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_category_id");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_categories_name");

                    b.HasIndex("ParentCategoryId")
                        .HasDatabaseName("ix_categories_parent_category_id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("AddressId")
                        .HasDatabaseName("ix_orders_address_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_orders_user_id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_order_items");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_order_items_order_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_order_items_product_id");

                    b.ToTable("order_items", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Inventory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("inventory")
                        .HasComment("Inventory must not be a negative number");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<Guid?>("ProductColorId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_color_id");

                    b.Property<Guid>("ProductLineId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_line_id");

                    b.Property<Guid?>("ProductSizeId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_size_id");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("ProductColorId")
                        .HasDatabaseName("ix_products_product_color_id");

                    b.HasIndex("ProductLineId")
                        .HasDatabaseName("ix_products_product_line_id");

                    b.HasIndex("ProductSizeId")
                        .HasDatabaseName("ix_products_product_size_id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductColor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_product_colors");

                    b.ToTable("product_colors", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_product_images");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_images_product_id");

                    b.ToTable("product_images", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1080)
                        .HasColumnType("character varying(1080)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("image_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_product_lines");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_product_lines_category_id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("ix_product_lines_title");

                    b.ToTable("product_lines", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductSize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_product_sizes");

                    b.ToTable("product_sizes", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ReviewImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uuid")
                        .HasColumnName("review_id");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_review_images");

                    b.HasIndex("ReviewId")
                        .HasDatabaseName("ix_review_images_review_id");

                    b.ToTable("review_images", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("avatar");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone_number");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_role");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("UserRole")
                        .HasDatabaseName("ix_users_user_role");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1080)
                        .HasColumnType("character varying(1080)")
                        .HasColumnName("comment");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsAnonymous")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_anonymous");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<Guid?>("ProductLineId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_line_id");

                    b.Property<int>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1)
                        .HasColumnName("rating")
                        .HasComment("Rating must be between 1 and 5");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_reviews");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_reviews_product_id");

                    b.HasIndex("ProductLineId")
                        .HasDatabaseName("ix_reviews_product_line_id");

                    b.HasIndex("UserId", "ProductId")
                        .IsUnique()
                        .HasDatabaseName("ix_reviews_user_id_product_id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Address", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_addresses_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Category", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_categories_categories_parent_category_id");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Order", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_addresses_address_id");

                    b.HasOne("Eshop.Core.src.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_users_user_id");

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.OrderItem", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_items_orders_order_id");

                    b.HasOne("Eshop.Core.src.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_items_products_product_id");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Product", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.ProductColor", "ProductColor")
                        .WithMany("Products")
                        .HasForeignKey("ProductColorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_products_product_colors_product_color_id");

                    b.HasOne("Eshop.Core.src.Entity.ProductLine", "ProductLine")
                        .WithMany("Products")
                        .HasForeignKey("ProductLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_products_product_lines_product_line_id");

                    b.HasOne("Eshop.Core.src.Entity.ProductSize", "ProductSize")
                        .WithMany("Products")
                        .HasForeignKey("ProductSizeId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_products_product_sizes_product_size_id");

                    b.Navigation("ProductColor");

                    b.Navigation("ProductLine");

                    b.Navigation("ProductSize");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductImage", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_images_products_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductLine", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.Category", "Category")
                        .WithMany("ProductLines")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_lines_categories_category_id");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ReviewImage", b =>
                {
                    b.HasOne("Review", "Review")
                        .WithMany("ReviewImages")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_review_images_reviews_review_id");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Review", b =>
                {
                    b.HasOne("Eshop.Core.src.Entity.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_products_product_id");

                    b.HasOne("Eshop.Core.src.Entity.ProductLine", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ProductLineId")
                        .HasConstraintName("fk_reviews_product_lines_product_line_id");

                    b.HasOne("Eshop.Core.src.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_users_user_id");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Category", b =>
                {
                    b.Navigation("ProductLines");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductColor", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductLine", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.ProductSize", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Eshop.Core.src.Entity.User", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Review", b =>
                {
                    b.Navigation("ReviewImages");
                });
#pragma warning restore 612, 618
        }
    }
}
