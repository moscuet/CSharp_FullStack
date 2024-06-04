
using Eshop.Core.src.ValueObject;

namespace Eshop.Core.src.Common
{
    public class QueryOptions
    {
        public int? Limit { get; set; } = 20;
        public int? StartingAfter { get; set; }
        public SortBy? SortBy { get; set; }
        public SortOrder? SortOrder { get; set; }
        public string? SearchKey { get; set; } = string.Empty;
        public string? CategoryId { get; set; }
        public string? PriceRange { get; set; }
    }

}


