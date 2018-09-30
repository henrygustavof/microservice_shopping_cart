namespace Product.Application.Service
{
    using System.Collections.Generic;
    using Dto;

    public interface IProductApplicationService
    {
        ProductOutputDto Get(int id);
        List<ProductOutputDto> GetAll();
        void Create(ProductCreateDto productCreateDto);

    }
}
