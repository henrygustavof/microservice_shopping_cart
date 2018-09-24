using System.Collections.Generic;
using AutoMapper;
using Product.Api.Product.Application.Dto;

namespace Product.Api.Product.Application.Assembler
{

    public class ProductCreateAssembler
    {
        private readonly IMapper _mapper;

        public ProductCreateAssembler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Domain.Entity.Product toEntity(ProductCreateDto productCreateDto)
        {
            return _mapper.Map<Domain.Entity.Product>(productCreateDto);
        }

        public ProductOutputDto fromEntity(Domain.Entity.Product product)
        {
            return _mapper.Map<ProductOutputDto>(product);
        }

        public List<ProductOutputDto> fromEntityList(List<Domain.Entity.Product> productList)
        {
            return _mapper.Map<List<ProductOutputDto>>(productList);
        }
    }
}
