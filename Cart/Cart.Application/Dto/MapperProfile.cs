namespace Cart.Application.Dto
{
    using AutoMapper;
    using Domain.Entity;
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
