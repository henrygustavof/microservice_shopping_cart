namespace Product.Application.Assembler
{
    using System.Collections.Generic;
    using AutoMapper;
    using Dto;
    using Domain.Entity;
    public class ProductCreateAssembler
    {
        private readonly IMapper _mapper;

        public ProductCreateAssembler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Product ToEntity(ProductCreateDto productCreateDto)
        {
            return _mapper.Map<Product>(productCreateDto);
        }

        public ProductOutputDto FromEntity(Product product)
        {
            return _mapper.Map<ProductOutputDto>(product);
        }

        public List<ProductOutputDto> FromEntityList(List<Product> productList)
        {
            return _mapper.Map<List<ProductOutputDto>>(productList);
        }
    }
}
