using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
using Eshop.Service.src.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<UserUpdateDTO, User>()
            .ForMember(dest => dest.FirstName, opts => opts.Condition(src => src.FirstName != null))
            .ForMember(dest => dest.LastName, opts => opts.Condition(src => src.LastName != null))
            .ForMember(dest => dest.Password, opts => opts.Condition(src => src.Password != null))
            .ForMember(dest => dest.Avatar, opts => opts.Condition(src => src.Avatar != null))
            .ForMember(dest => dest.DateOfBirth, opts => opts.Condition(src => src.DateOfBirth.HasValue))
            .ForMember(dest => dest.UserRole, opts => opts.Condition(src => src.UserRole.HasValue))
            .ForMember(dest => dest.PhoneNumber, opts => opts.Condition(src => src.PhoneNumber != null));

        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();

        // Address mappings
        CreateMap<Address, AddressReadDTO>();
        CreateMap<AddressCreateDTO, Address>();
        CreateMap<AddressUpdateDTO, Address>()
             .ForMember(dest => dest.Street, opts => opts.Condition(src => src.Street != null))
             .ForMember(dest => dest.House, opts => opts.Condition(src => src.House != null))
             .ForMember(dest => dest.City, opts => opts.Condition(src => src.City != null))
             .ForMember(dest => dest.ZipCode, opts => opts.Condition(src => src.ZipCode != null))
             .ForMember(dest => dest.Country, opts => opts.Condition(src => src.Country != null))
             .ForMember(dest => dest.PhoneNumber, opts => opts.Condition(src => src.PhoneNumber != null));

        // Product mappings
        CreateMap<Product, ProductReadDTO>()
            .ForMember(dest => dest.ProductLineName, opts => opts.MapFrom(src => src.ProductLine.Title))
            .ForMember(dest => dest.SizeValue, opts => opts.MapFrom(src => src.ProductSize != null ? src.ProductSize.Value : null))
            .ForMember(dest => dest.ColorValue, opts => opts.MapFrom(src => src.ProductColor != null ? src.ProductColor.Value.ToString() : null))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => src.ProductImages)); // Ensure the mapping for images

        CreateMap<ProductCreateDTO, Product>();
        CreateMap<ProductUpdateDTO, Product>()
            .ForMember(dest => dest.ProductLineId, opts => opts.Condition(src => src.ProductLineId.HasValue))
            .ForMember(dest => dest.ProductSizeId, opts => opts.Condition(src => src.SizeId.HasValue))
            .ForMember(dest => dest.ProductColorId, opts => opts.Condition(src => src.ColorId.HasValue))
            .ForMember(dest => dest.Inventory, opts => opts.Condition(src => src.Inventory.HasValue));

        // ProductLine mappings
        CreateMap<ProductLine, ProductLineReadDTO>()
            .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name));
        CreateMap<ProductLineCreateDTO, ProductLine>();
        CreateMap<ProductLineUpdateDTO, ProductLine>()
            .ForMember(dest => dest.Title, opts => opts.Condition(src => src.Title != null))
            .ForMember(dest => dest.Description, opts => opts.Condition(src => src.Description != null))
            .ForMember(dest => dest.Price, opts => opts.Condition(src => src.Price.HasValue))
            .ForMember(dest => dest.CategoryId, opts => opts.Condition(src => src.CategoryId.HasValue));

        // Order mappings
        CreateMap<Order, OrderReadDTO>();
        CreateMap<OrderCreateDTO, Order>();
        CreateMap<OrderUpdateDTO, Order>()
                    .ForMember(dest => dest.AddressId, opts => opts.Condition(src => src.AddressId.HasValue))
                    .ForMember(dest => dest.Status, opts => opts.Condition(src => src.Status.HasValue))
                    .ForMember(dest => dest.Total, opts => opts.Condition(src => src.Total.HasValue));

        // OrderItem mappings
        CreateMap<OrderItem, OrderItemReadDTO>();
        CreateMap<OrderItemCreateDTO, OrderItem>();
        CreateMap<OrderItemUpdateDTO, OrderItem>()
                    .ForMember(dest => dest.Quantity, opts => opts.Condition(src => src.Quantity > 0))  // Ensure quantity is greater than 0
                    .ForMember(dest => dest.Price, opts => opts.Condition(src => src.Price > 0)); // Ensure price is greater than 0

        // Category mappings
        CreateMap<Category, CategoryReadDTO>();
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>()
            .ForMember(dest => dest.Name, opts => opts.Condition(src => src.Name != null))
            .ForMember(dest => dest.ParentCategoryId, opts => opts.Condition(src => src.ParentCategoryId.HasValue))
            .ForMember(dest => dest.ImageUrl, opts => opts.Condition(src => src.ImageUrl != null));





        // Custom mapping for ReviewCreateControllerDTO to ReviewCreateDTO
        CreateMap<ReviewCreateControllerDTO, ReviewCreateDTO>();
      CreateMap<ReviewCreateDTO, Review>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.ReviewImages, opt => opt.Ignore());


        CreateMap<Review, ReviewReadDTO>()
                   .ForMember(dest => dest.Images, opts => opts.MapFrom(src => src.ReviewImages));



        // ProductColor mappings
        CreateMap<ProductColor, ProductColorReadDTO>();
        CreateMap<ProductColorCreateDTO, ProductColor>();
        CreateMap<ProductColorUpdateDTO, ProductColor>()
            .ForMember(dest => dest.Value, opts => opts.Condition(src => src.Value != null));

        // ProductSize mappings
        CreateMap<ProductSize, ProductSizeReadDTO>();
        CreateMap<ProductSizeCreateDTO, ProductSize>();
        CreateMap<ProductSizeUpdateDTO, ProductSize>()
            .ForMember(dest => dest.Value, opts => opts.Condition(src => src.Value != null));
    }
}

