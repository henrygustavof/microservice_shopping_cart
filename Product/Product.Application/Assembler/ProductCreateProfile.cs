namespace Product.Application.Assembler
{
    using AutoMapper;
    using Product.Domain.ValueObject;
    using Dto;
    using Domain.Entity;
    public class ProductCreateProfile : Profile
    {
        public ProductCreateProfile()
        {
            CreateMap<ProductCreateDto, Product>()
                .ForMember(
                    dest => dest.Balance,
                    opts => opts.MapFrom(
                        src => new Money(src.Balance, src.Currency)
                    )
                )
               .ReverseMap();

            CreateMap<ProductOutputDto, Product>()
                .ForMember(
                    dest => dest.Balance,
                    opts => opts.MapFrom(
                        src => new Money(src.Balance, src.Currency)
                    )
                )
                .ReverseMap();
        }
    }
}
