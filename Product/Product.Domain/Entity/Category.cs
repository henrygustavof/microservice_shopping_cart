namespace Product.Domain.Entity
{
    public class Category
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public Category()
        {

        }
    }
}
