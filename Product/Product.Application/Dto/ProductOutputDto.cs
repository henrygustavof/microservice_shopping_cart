namespace Product.Application.Dto
{
    using Product.Domain.Enum;

    public class ProductOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Unit { get; set; }
        public int CategoryId { get; set; }

        public string Currency { get; set; }
    }
}
