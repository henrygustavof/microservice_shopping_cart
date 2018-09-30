using Product.Api.Common.Domain.Enum;

namespace Product.Api.Product.Application.Dto
{
    public class ProductOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
    }
}
