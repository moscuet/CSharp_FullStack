using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

    public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<UserUpdateDTO, User>();

        // Address mappings
        CreateMap<Address, AddressReadDTO>();
        CreateMap<AddressCreateDTO, Address>();
        CreateMap<AddressUpdateDTO, Address>();

        // Product mappings
        CreateMap<Product, ProductReadDTO>();
        CreateMap<ProductCreateDTO, Product>();
        CreateMap<ProductUpdateDTO, Product>();

        // Order mappings
        CreateMap<Order, OrderReadDTO>();
        CreateMap<OrderCreateDTO, Order>();
        CreateMap<OrderUpdateDTO, Order>();

        // OrderItem mappings
        CreateMap<OrderItem, OrderItemReadDTO>();
        CreateMap<OrderItemCreateDTO, OrderItem>();

        // Category mappings
        CreateMap<Category, CategoryReadDTO>();
        CreateMap<CategoryCreateDTO, Category>();

        // Review mappings
        CreateMap<Review, ReviewReadDTO>();
        CreateMap<ReviewCreateDTO, Review>();
        CreateMap<ReviewUpdateDTO, Review>();

        // Wishlist mappings
        CreateMap<Wishlist, WishlistReadDTO>();
        CreateMap<WishlistCreateDTO, Wishlist>();
    }
}

