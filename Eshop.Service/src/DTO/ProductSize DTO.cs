namespace Eshop.Service.src.DTO
{
    public class ProductSizeCreateDTO
    {
        public string Value { get; set; }
    }

    public class ProductSizeReadDTO
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    public class ProductSizeUpdateDTO
    {
        public string Value { get; set; }
    }
}
