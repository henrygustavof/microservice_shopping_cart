namespace Product.Domain.Entity
{
    using ValueObject;

    public class Product
    {
        public virtual long Id { get; set; }

        public virtual string  Name { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual string  Description { get; set; }
        
        public virtual Money Balance { get; set; }

        public virtual int Unit { get; set; }
        public virtual Category Category { get; set; }
        public Product()
        {
        }
    }
}
