namespace Product.Application.Contracts
{
    using System.Collections.Generic;
    using Dto;

    public interface IProductApplicationService
    {
        ProductOutputDto Get(int id);
        List<ProductOutputDto> GetAll();
        void Create(ProductCreateDto productCreateDto);

        PaginationOutputDto GetAll(int page, int pageSize, string sortBy, string sortDirection);

    }
}
