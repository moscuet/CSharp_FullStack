namespace Eshop.Service.src.DTO
{
    public class ProductLineCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class ProductLineReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
         public List<string> ImageUrls { get; set; } = new List<string>();
        public List<ProductReadDTO> Products { get; set; } = new List<ProductReadDTO>();
    }

    public class ProductLineUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
    }

}
