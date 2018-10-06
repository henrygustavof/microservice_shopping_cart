namespace Cart.Application.Dto
{
    using AutoMapper;
    using Domain.Entity;
    using System.Linq;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cart, CartOutputDto>()
                .ForMember(
                dest => dest.Total,
                opts => opts.MapFrom(
                    src => src.Items.Any() ? src.Items.Sum(item => item.UnitPrice * item.Unit) : 0)
            );

            CreateMap<CartItem, CartItemOutputDto>()
                .ForMember(
                    dest => dest.Total,
                    opts => opts.MapFrom(
                        src => src.Unit * src.UnitPrice)
                );

            CreateMap<CartItemCreateDto, CartItem>();
        }
    }
}
