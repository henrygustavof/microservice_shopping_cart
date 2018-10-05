namespace Cart.Application.Dto
{
    using AutoMapper;
    using Domain.Entity;
    using System.Linq;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CartDto, Cart>();
            CreateMap<Cart, CartDto>()
                .ForMember(
                dest => dest.Total,
                opts => opts.MapFrom(
                    src => src.Items.Any() ? src.Items.Sum(item => item.UnitPrice * item.Quantity) : 0)
            );

            CreateMap<CartItem, CartItemDto>()
                .ForMember(
                    dest => dest.Total,
                    opts => opts.MapFrom(
                        src => src.Quantity * src.UnitPrice)
                  ).ReverseMap();
        }
    }
}
