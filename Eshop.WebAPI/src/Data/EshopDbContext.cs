using System.ComponentModel.DataAnnotations;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Data
{
      public class EshopDbContext : DbContext
      {
            protected readonly IConfiguration _config;
            private readonly ILoggerFactory _loggerFactory;

            public DbSet<User>? Users { get; set; }
            public DbSet<Category>? Categories { get; set; }
            public DbSet<Address>? Addresses { get; set; }
            public DbSet<ReviewImage>? ReviewImages { get; set; }
            public DbSet<ProductImage>? ProductImages { get; set; }

            public DbSet<Review>? Reviews { get; set; }
            public DbSet<ProductLine>? ProductLines { get; set; }
            public DbSet<Product>? Products { get; set; }
            public DbSet<OrderItem>? OrderItems { get; set; }
            public DbSet<Order>? Orders { get; set; }
            public DbSet<ProductColor>? ProductColors { get; set; }
            public DbSet<ProductSize>? ProductSizes { get; set; }




            static EshopDbContext()
            {
                  AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
                  AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
            public EshopDbContext(DbContextOptions options, IConfiguration config, ILoggerFactory loggerFactory) : base(options)
            {
                  _config = config;
                  _loggerFactory = loggerFactory;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                  optionsBuilder
                  // .AddInterceptors(new TimeStampInterceptor())
                  .UseSnakeCaseNamingConvention();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  modelBuilder.HasPostgresExtension("uuid-ossp");
                  // craete enum table
                  modelBuilder.HasPostgresEnum<UserRole>();
                  modelBuilder.HasPostgresEnum<OrderStatus>();
                  modelBuilder.HasPostgresEnum<SortBy>();
                  modelBuilder.HasPostgresEnum<SortOrder>();
                  modelBuilder.HasPostgresEnum<TokenType>();
                  modelBuilder.HasPostgresEnum<ColorValue>();
                  modelBuilder.HasPostgresEnum<EntityType>();


                  modelBuilder.Entity<User>(entity =>
                  {
                        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.Email).IsRequired().HasMaxLength(100).IsUnicode(false);
                        entity.HasIndex(e => e.Email).IsUnique();
                        entity.Property(e => e.Password).HasMaxLength(255);
                        entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                        entity.Property(e => e.Avatar).HasMaxLength(2048);
                        entity.Property(e => e.UserRole).HasConversion<string>();
                        entity.HasIndex(e => e.UserRole);
                  });

                  modelBuilder.Entity<Category>(entity =>
                  {
                        entity.Property(e => e.Name).IsRequired().HasMaxLength(128);
                        entity.Property(e => e.ImageUrl).IsRequired().IsUnicode(false);
                        entity.HasIndex(e => e.Name).IsUnique();
                        entity.Property(e => e.ParentCategoryId).IsRequired(false);
                        entity.HasOne(e => e.ParentCategory)
                        .WithMany(e => e.SubCategories)
                        .HasForeignKey(e => e.ParentCategoryId)
                        .OnDelete(DeleteBehavior.Restrict);
                        entity.HasMany(e => e.ProductLines)
                        .WithOne(pl => pl.Category)
                        .HasForeignKey(pl => pl.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);
                  });

                  modelBuilder.Entity<Address>(entity =>
                      {
                            entity.Property(e => e.Street).IsRequired().HasMaxLength(100);
                            entity.Property(e => e.House).IsRequired().HasMaxLength(100);
                            entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                            entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(20);
                            entity.Property(e => e.Country).IsRequired().HasMaxLength(50);
                            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                            entity.HasOne(e => e.User)
                          .WithMany(u => u.Addresses)
                          .HasForeignKey(e => e.UserId)
                          .IsRequired()
                          .OnDelete(DeleteBehavior.Cascade);
                      });


                  modelBuilder.Entity<ProductLine>(entity =>
                     {
                           entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                           entity.HasIndex(e => e.Title).IsUnique();
                           entity.Property(e => e.Description).IsRequired().HasMaxLength(1080);
                           entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                           entity.HasOne(e => e.Category)
                           .WithMany(c => c.ProductLines)
                           .HasForeignKey(e => e.CategoryId)
                           .IsRequired()
                           .OnDelete(DeleteBehavior.Cascade);
                           entity.HasMany(e => e.Products)
                           .WithOne(p => p.ProductLine)
                           .HasForeignKey(p => p.ProductLineId)
                           .OnDelete(DeleteBehavior.Cascade);
                     });



                  // ProductSize Configuration
                  modelBuilder.Entity<ProductSize>(entity =>
                  {
                        entity.Property(e => e.Value).IsRequired();
                        entity.HasMany(e => e.Products)
                .WithOne(p => p.ProductSize)
                .HasForeignKey(p => p.ProductSizeId);
                  });

                  // ProductColor Configuration
                  modelBuilder.Entity<ProductColor>(entity =>
                  {
                        entity.HasMany(e => e.Products)
                .WithOne(p => p.ProductColor)
                .HasForeignKey(p => p.ProductColorId);
                  });


                  // Order Configuration
                  modelBuilder.Entity<Order>(entity =>
                  {
                        entity.Property(e => e.Status).IsRequired();
                        entity.Property(e => e.Total).IsRequired().HasColumnType("decimal(18,2)");
                        entity.HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
                        entity.HasOne(e => e.Address)
                        .WithMany()
                        .HasForeignKey(e => e.AddressId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
                        entity.HasMany(e => e.Items)
                        .WithOne(i => i.Order)
                        .HasForeignKey(i => i.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);
                  });

                  // OrderItem Configuration
                  modelBuilder.Entity<OrderItem>(entity =>
                  {
                        entity.Property(e => e.Quantity)
                        .IsRequired()
                        .HasColumnType("int");

                        entity.Property(e => e.Price)
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                        entity.HasOne(e => e.Order)
                        .WithMany(o => o.Items)
                        .HasForeignKey(e => e.OrderId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

                        entity.HasOne(e => e.Product)
                        .WithMany()
                        .HasForeignKey(e => e.ProductId);
                  });


                  // Product Configuration
                  modelBuilder.Entity<Product>(entity =>
                  {
                        entity.ToTable("products");

                        entity.HasKey(e => e.Id);

                        entity.Property(e => e.ProductLineId)
            .IsRequired();

                        entity.Property(e => e.ProductSizeId);

                        entity.Property(e => e.ProductColorId);

                        entity.Property(e => e.Inventory)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Inventory must not be a negative number");

                        entity.HasOne(e => e.ProductLine)
            .WithMany(pl => pl.Products)
            .HasForeignKey(e => e.ProductLineId)
            .OnDelete(DeleteBehavior.Cascade);

                        entity.HasOne(e => e.ProductSize)
            .WithMany(ps => ps.Products)
            .HasForeignKey(e => e.ProductSizeId)
            .OnDelete(DeleteBehavior.SetNull);

                        entity.HasOne(e => e.ProductColor)
            .WithMany(pc => pc.Products)
            .HasForeignKey(e => e.ProductColorId)
            .OnDelete(DeleteBehavior.SetNull);

                        entity.HasMany(e => e.ProductImages)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

                        entity.HasMany(e => e.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);
                  });



                  // Review Configuration
                  modelBuilder.Entity<Review>(entity =>
      {
            entity.ToTable("reviews");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
              .IsRequired();

            entity.Property(e => e.ProductId)
              .IsRequired();

            entity.Property(e => e.Comment)
              .IsRequired()
              .HasMaxLength(1080);

            entity.Property(e => e.Rating)
              .IsRequired()
              .HasDefaultValue(1)
              .HasComment("Rating must be between 1 and 5");

            entity.Property(e => e.IsAnonymous)
              .IsRequired()
              .HasDefaultValue(false);

            entity.HasOne(e => e.Product)
              .WithMany(p => p.Reviews)
              .HasForeignKey(e => e.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
              .WithMany()
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.ReviewImages)
              .WithOne(ri => ri.Review)
              .HasForeignKey(ri => ri.ReviewId)
              .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UserId, e.ProductId }).IsUnique(); // Composite unique key
      });

                  // ReviewImage Configuration
                  modelBuilder.Entity<ReviewImage>(entity =>
                  {
                        entity.ToTable("review_images");

                        entity.HasKey(e => e.Id);

                        entity.Property(e => e.ReviewId)
            .IsRequired();

                        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(2048);

                        entity.HasOne(e => e.Review)
            .WithMany(r => r.ReviewImages)
            .HasForeignKey(e => e.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
                  });

                  // ProductImage Configuration
                  modelBuilder.Entity<ProductImage>(entity =>
                  {
                        entity.ToTable("product_images");

                        entity.HasKey(e => e.Id);

                        entity.Property(e => e.ProductId)
            .IsRequired();

                        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(2048);

                        entity.HasOne(e => e.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
                  });
            }


            public override int SaveChanges()
            {
                  var entities = from e in ChangeTracker.Entries()
                                 where e.State == EntityState.Added || e.State == EntityState.Modified
                                 select e.Entity;

                  foreach (var entity in entities)
                  {
                        var validationContext = new ValidationContext(entity);
                        Validator.ValidateObject(entity, validationContext);
                  }

                  return base.SaveChanges();
            }

            public async Task InitializeDatabaseAsync()
            {
                  var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "src/Data/SqlScripts", "review_functions.sql");
                  if (File.Exists(scriptPath))
                  {
                        var script = await File.ReadAllTextAsync(scriptPath);
                        await Database.ExecuteSqlRawAsync(script);
                  }
                  else
                  {
                        Console.Error.WriteLine("Failed to find SQL script for initializing database functions.");
                  }
            }
      }
}
