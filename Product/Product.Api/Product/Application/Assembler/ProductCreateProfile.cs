using AutoMapper;
using Product.Api.Common.Domain.ValueObject;
using Product.Api.Product.Application.Dto;

namespace Product.Api.Product.Application.Assembler
{
    public class ProductCreateProfile : Profile
    {
        public ProductCreateProfile()
        {
            CreateMap<ProductCreateDto, Domain.Entity.Product>()
                .ForMember(
                    dest => dest.Balance,
                    opts => opts.MapFrom(
                        src => new Money(src.Balance, src.Currency)
                    )
                )
               .ReverseMap();

            CreateMap<ProductOutputDto, Domain.Entity.Product>()
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
