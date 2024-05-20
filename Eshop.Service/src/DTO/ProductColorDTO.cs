namespace Eshop.Service.src.DTO
{
    public class ProductColorCreateDTO
    {
        public string Value { get; set; }
    }

    public class ProductColorReadDTO
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    public class ProductColorUpdateDTO
    {
        public string Value { get; set; }
    }
}
