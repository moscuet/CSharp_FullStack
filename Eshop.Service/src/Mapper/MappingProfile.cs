using AutoMapper;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

    public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<UserUpdateDTO, User>()
            .ForMember(dest => dest.FirstName, opts => opts.Condition(src => src.FirstName != null))
            .ForMember(dest => dest.LastName, opts => opts.Condition(src => src.LastName != null))
            .ForMember(dest => dest.Password, opts => opts.Ignore())
            .ForMember(dest => dest.Avatar, opts => opts.Condition(src => src.Avatar != null))
            .ForMember(dest => dest.DateOfBirth, opts => opts.Condition(src => src.DateOfBirth.HasValue))
            .ForMember(dest => dest.UserRole, opts => opts.Condition(src => src.UserRole.HasValue))
            .ForMember(dest => dest.PhoneNumber, opts => opts.Condition(src => src.PhoneNumber != null));

        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();

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

    }
}

