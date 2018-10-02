namespace Product.Application.Dto
{
    using Product.Domain.Enum;

    public class ProductCreateDto
    {
        public  string Name { get; set; }
        public  string PictureUrl { get; set; }
        public  string Description { get; set; }
        public decimal Balance { get; set; }

        public int Unit { get; set; }
        public int CategoryId { get; set; }
        public Currency Currency { get; set; }
    }
}
