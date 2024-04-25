using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;

namespace BookBurrowAPI.Interfaces
{
    public interface IGroupRepository
    {
        bool SaveChanges();
        ICollection<PrivateGroups> FindGroupsByName(string groupName);
        PrivateGroups? GetGroup(int id);
        bool GroupExist(int id);
        bool UserExist(PGUserNames user);
        ICollection<UsersPGUserNames>? GetGroupUsers(int id);
        bool? AddUsers(PGUserNames user);
        int CreateGroup(PrivateGroups names);
        bool UpdateGroup(PrivateGroups info);
        bool UpdateUserName(PGUserNames name);
        bool DeleteGroup(int chatId);
        bool DeleteUserName(PGUserNames user);
    }
}
