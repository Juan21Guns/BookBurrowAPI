using AutoMapper;
using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;

namespace BookBurrowAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UsersDto>();
            CreateMap<UsersDto, Users>();
            CreateMap<FriendsListDto, FriendsList>();
            CreateMap<PrivateGroups, PrivateGroupsDto>();
            CreateMap<PGUserNames, PGUserNamesDto>();
        }
    }
}
