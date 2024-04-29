using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;
using BookBurrowAPI.Repositories;

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
            CreateMap<PrivateGroupsDto, PrivateGroups>();
            CreateMap<PGUserNamesDto, PGUserNames>();
            CreateMap<UsersPGUserNames, PGUserNames>();
            CreateMap<Messages, MessageDto>();
            CreateMap<MessageDto, Messages>();
        }
    }
}
