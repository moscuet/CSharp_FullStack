using Eshop.Core.src.ValueObject;

public class ImageCreateDTO
{
    public Guid EntityId { get; set; }
    public EntityType EntityType { get; set; }
    public string Url { get; set; }
}

public class ImageUpdateDTO
{
    public string Url { get; set; }
}

public class ImageReadDTO
{
    public Guid Id { get; set; }
    public string Url { get; set; }
}
