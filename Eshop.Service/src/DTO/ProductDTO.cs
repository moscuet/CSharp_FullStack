
using Eshop.Core.src.Entity;


namespace Eshop.Service.src.DTO;
public class ProductCreateDTO
{
    public Guid ProductLineId { get; set; }
    public Guid? ProductSizeId { get; set; }
    public Guid? ProductColorId { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
}

public class ProductReadDTO
{
    public Guid Id { get; set; }
    public Guid ProductLineId { get; set; }
    public Guid? ProductSizeId { get; set; }
    public Guid? ProductColorId { get; set; }
    public int Inventory { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<ProductImage> Images { get; set; }
    public IEnumerable<Review> Reviewss { get; set; }
    public string ProductLineName { get; set; }
    public string ProductSizeValue { get; set; }
    public string ProductColorValue { get; set; }
}


public class ProductUpdateDTO
{
    public Guid? ProductLineId { get; set; }
    public Guid? ProductSizeId { get; set; }
    public Guid? ProductColorId { get; set; }
    public int? Inventory { get; set; }
    public decimal? Price { get; set; }

}
