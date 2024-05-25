namespace Eshop.Service.src.DTO
{
    public class ProductLineCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }

    }
   public class ProductLineReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProductReadDTO> Products { get; set; } = new List<ProductReadDTO>();
        public List<ReviewReadDTO> Reviews { get; set; } = new List<ReviewReadDTO>();
    }

    public class ProductLineUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
