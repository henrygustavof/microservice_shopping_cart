namespace Product.Application.Assembler
{
    using AutoMapper;
    using Product.Application.Dto;
    using Product.Domain.Entity;

    public class CategoryCreateProfile: Profile
    {
        public CategoryCreateProfile()
        {
            CreateMap<CategoryCreateDto, Category>().ReverseMap();

            CreateMap<CategoryOutputDto, Category>().ReverseMap();
        }     
    }
}
