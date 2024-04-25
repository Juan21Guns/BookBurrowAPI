using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.MappingDto;
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

        public ICollection<PrivateGroups> FindGroupsByName(string groupName)
        {
            return _context.PrivateGroups
                .Where(c => c.ChatName == groupName)
                .ToList();
        }

        public bool SaveChanges()
        {
            try
            {
                int didSave = _context.SaveChanges();
                return didSave > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public PrivateGroups? GetGroup(int id)
        {
            if (!_context.PrivateGroups.Any(c => c.ChatId == id))
            {
                return null;
            }
            return _context.PrivateGroups.Where(c => c.ChatId == id).First();
        }

        public ICollection<UsersPGUserNames>? GetGroupUsers(int id)
        {
            if (GetGroup(id) == null)
            {
                return null;
            }

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

        public bool AddUsers(PGUserNames user)
        {
            if (user == null)
            {
                return false;
            }

            _context.Add(user);
            return SaveChanges();
        }

        public int CreateGroup(PrivateGroups names)
        {
            if (names == null)
            {
                return 0;
            }

            _context.PrivateGroups.Add(names);
            if (!SaveChanges())
            {
                return -1;
            };

            PGUserNames newPGUserName = new PGUserNames()
            {
                UserId = names.GroupAdmin,
                ChatId = names.ChatId,
                Username = "Admin"
            };

            _context.Add(newPGUserName);

            if (!SaveChanges())
            {
                return -1;
            };
            return names.ChatId;
        }
        //PrivateGroups UpdateGroup(int bookId, PGUserNames groupUsers);
        //PrivateGroups DeleteGroup(int bookId);
        //PGUserNames CreateUserName(string userName);
        //PGUserNames UpdateUserName(string userName);
        //PGUserNames DeleteUserName(int bookId);
    }
}
