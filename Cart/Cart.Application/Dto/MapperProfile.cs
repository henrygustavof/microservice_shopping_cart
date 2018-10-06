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
                    src => src.Items.Any() ? src.Items.Sum(item => item.UnitPrice * item.Quantity) : 0)
            );

            CreateMap<CartItem, CartItemOutputDto>()
                .ForMember(
                    dest => dest.Total,
                    opts => opts.MapFrom(
                        src => src.Quantity * src.UnitPrice)
                );

            CreateMap<CartItemCreateDto, CartItem>();
        }
    }
}
