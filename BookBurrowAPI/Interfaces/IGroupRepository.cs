using BookBurrowAPI.Models;

namespace BookBurrowAPI.Interfaces
{
    public interface IGroupRepository
    {
        bool SaveChanges();
        ICollection<PrivateGroups> FindGroupsByName(string groupName);
        PrivateGroups? GetGroup(int id);
        ICollection<UsersPGUserNames>? GetGroupUsers(int id);
        bool AddUsers(PGUserNames user);
        int CreateGroup(PrivateGroups names);
        //PrivateGroups UpdateGroup(int bookId, PGUserNames groupUsers);
        //PrivateGroups DeleteGroup(int bookId);
        //PGUserNames CreateUserName(string userName);
        //PGUserNames UpdateUserName(string userName);
        //PGUserNames DeleteUserName(int bookId);
    }
}
