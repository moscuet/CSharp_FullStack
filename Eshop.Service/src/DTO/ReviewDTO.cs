using Eshop.Core.src.Entity;

namespace Eshop.Service.src.DTO;


public class ReviewCreateControllerDTO
{
    public Guid ProductId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public bool IsAnonymous { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
}

public class ReviewCreateDTO
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public bool IsAnonymous { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
}


public class ReviewReadDTO
{
    public Guid Id { get; set; }
      public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public bool IsAnonymous { get; set; }
    public List<ReviewImage> Images { get; set; }
}

public class ReviewUpdateDTO
{
    public string? Comment { get; set; }
    public int? Rating { get; set; }
    public bool? IsAnonymous { get; set; }
}
