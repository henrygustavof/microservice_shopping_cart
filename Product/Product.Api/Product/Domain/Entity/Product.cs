using Product.Api.Common.Domain.ValueObject;

namespace Product.Api.Product.Domain.Entity
{
    public class Product
    {
        public virtual long Id { get; set; }

        public virtual string  Name { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual string  Description { get; set; }
        
        public virtual Money Balance { get; set; }
        public Product()
        {
        }
    }
}
