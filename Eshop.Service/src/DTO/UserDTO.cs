using Eshop.Core.src.Common;
using Eshop.Core.src.ValueObject;

namespace Eshop.Service.src.DTO;

public class UserCreateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Avatar { get; set; } = AppConstants.AVATAR_DEFAULT_IMAGE;
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public UserRole UserRole { get; set; } = UserRole.User;
}

public class UserReadDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
    public DateOnly DateOfBirth { get; set; }
}

public class UserUpdateDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public string? Avatar { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public UserRole? UserRole { get; set; }
    public string? PhoneNumber { get; set; }
}
