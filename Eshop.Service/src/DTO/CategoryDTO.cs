namespace Eshop.Service.src.DTO;

public class CategoryCreateDTO
{
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string ImageUrl { get; set; }
}

public class CategoryReadDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid  ParentCategoryId { get; set; }
    public string  ImageUrl { get; set; }
}

public class CategoryUpdateDTO
{
    public string? Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string? ImageUrl { get; set; }
}
