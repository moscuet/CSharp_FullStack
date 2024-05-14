using Eshop.Core.src.Entity;

namespace Eshop.Service.src.DTO;
public class ProductCreateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public Guid CategoryId { get; set; }

}

public class ProductReadDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public string CategoryName { get; set; }

}

public class ProductUpdateDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Inventory { get; set; }
}
