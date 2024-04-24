using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BookBurrowAPI.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;
        public GroupRepository(DataContext context)
        {
            _context = context;
        }
        public PrivateGroups GetGroup(int id)
        {
            return _context.PrivateGroups.Where(c => c.ChatId == id).First();
        }

        public ICollection<UsersPGUserNames> GetGroupUsers(int id)
        {
            var fullGroup = GetGroup(id);

            ICollection<UsersPGUserNames> userNames = _context.PGUserNames
                .Where(c => c.ChatId == id)
                .Join(_context.Users,
                    pg => pg.UserId,
                    u => u.UserId,
                    (pgusername, user) => new
                    UsersPGUserNames
                    {
                        Username = pgusername.Username,
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    })
                .ToList();

            return userNames;
        }
/*        PrivateGroup CreateGroup(int bookId, PGUserNames groupUsers);
        PrivateGroup UpdateGroup(int bookId, PGUserNames groupUsers);
        PrivateGroup DeleteGroup(int bookId);
        PGUserNames CreateUserName(string userName);
        PGUserNames UpdateUserName(string userName);
        PGUserNames DeleteUserName(int bookId);*/
    }
}
