namespace Product.Application.Assembler
{
    using System.Collections.Generic;
    using AutoMapper;
    using Dto;
    using Domain.Entity;

    public class CategoryCreateAssembler
    {
        private readonly IMapper _mapper;

        public CategoryCreateAssembler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Category ToEntity(CategoryCreateDto CategoryCreateDto)
        {
            return _mapper.Map<Category>(CategoryCreateDto);
        }

        public CategoryOutputDto FromEntity(Category Category)
        {
            return _mapper.Map<CategoryOutputDto>(Category);
        }

        public List<CategoryOutputDto> FromEntityList(List<Category> CategoryList)
        {
            return _mapper.Map<List<CategoryOutputDto>>(CategoryList);
        }
    }
}
