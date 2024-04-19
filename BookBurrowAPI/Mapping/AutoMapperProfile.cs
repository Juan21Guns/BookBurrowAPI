using AutoMapper;
using BookBurrowAPI.Models;

namespace BookBurrowAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UsersDto>();
        }
    }
}
