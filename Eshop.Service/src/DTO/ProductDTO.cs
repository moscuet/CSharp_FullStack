
namespace Eshop.Service.src.DTO;
 public class ProductCreateDTO
    {
        public Guid ProductLineId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public int Inventory { get; set; }
    }


 public class ProductReadDTO
    {
        public Guid Id { get; set; }
        public Guid ProductLineId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public int Inventory { get; set; }
        public string ProductLineName { get; set; }
        public string? SizeValue { get; set; }
        public string? ColorValue { get; set; }
        public IEnumerable<ImageReadDTO> Images { get; set; } 
    }

public class ProductUpdateDTO
    {
        public Guid? ProductLineId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public int? Inventory { get; set; }
    }
