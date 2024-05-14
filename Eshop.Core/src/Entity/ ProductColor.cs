using Eshop.Core.src.ValueObject;

namespace Eshop.Core.src.Entity;


    public class ProductColor : BaseEntity
    {
        public ColorValue Value { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }