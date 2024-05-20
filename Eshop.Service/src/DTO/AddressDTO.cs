namespace Eshop.Service.src.DTO;

public class AddressCreateDTO
{
    public Guid? UserId { get; set; }
    public string Street { get; set; }
    public string House { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
}

public class AddressReadDTO
{
public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Street { get; set; }
    public string House { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
}

public class AddressUpdateDTO
{
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
