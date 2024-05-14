using Eshop.Core.src.ValueObject;

namespace Eshop.Service.src.DTO;
public class OrderCreateDTO
{
    public Guid UserId { get; set; }
    public Guid AddressId { get; set; }
    public List<OrderItemCreateDTO> Items { get; set; }

}

public class OrderReadDTO
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemReadDTO> Items { get; set; }
}

public class OrderUpdateDTO
{
    public Guid? AddressId { get; set; }
    public OrderStatus? Status { get; set; }
    public decimal? Total { get; set; }
}


public class OrderItemCreateDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}


public class OrderItemReadDTO
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class OrderItemUpdateDTO
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
