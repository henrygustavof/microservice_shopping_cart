namespace Identity.Application.Dto
{
    using AutoMapper;
    using Identity.Domain.Entity;

    public class MapperDto : Profile
    {
        public MapperDto()
        {
            CreateMap<User, Input.UserDto>().ReverseMap();
            CreateMap<User, Output.UserDto>().ReverseMap();

            CreateMap<Role, Input.RoleDto>().ReverseMap();
            CreateMap<Role, Output.RoleDto>().ReverseMap();

        }
    }
}
