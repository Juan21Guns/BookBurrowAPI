using BookBurrowAPI.Models;

namespace BookBurrowAPI.Interfaces
{
    public interface IGroupRepository
    {
        PrivateGroups GetGroup(int id);
        ICollection<UsersPGUserNames> GetGroupUsers(int id);
/*        PrivateGroup CreateGroup(int bookId, PGUserNames groupUsers);
        PrivateGroup UpdateGroup(int bookId, PGUserNames groupUsers);
        PrivateGroup DeleteGroup(int bookId);
        PGUserNames CreateUserName (string userName);
        PGUserNames UpdateUserName (string userName);
        PGUserNames DeleteUserName (int bookId);*/
    }
}
