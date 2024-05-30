using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BookBurrowAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
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

        public bool UserExists(string sub)
        {
            return _context.Users.Any(c => c.Sub == sub) == true;
        }

        public Users GetUser(string sub)
        {
            return _context.Users.Where(r => sub == r.Sub).FirstOrDefault();
        }

        public ICollection<Users>? GetUserByName(string? firstName, string? lastName)
        {
            if (firstName == null)
            {
                if (lastName != null)
                {
                    return _context.Users.Where(r => lastName == r.LastName).ToList();
                }

                return null;
            } else if (lastName == null)
            {
                return _context.Users.Where(r => firstName == r.FirstName).ToList();
            }

            return _context.Users
                .Where(r => (lastName == r.LastName) || (firstName == r.FirstName))
                .ToList();
        }

        public ICollection<Users> GetAllUsers(int startN, int endN, int friendId, int friendStatus)
        {
            var friendsList = _context.FriendsList
                .Where(c => ( (c.User1 == friendId) || (c.User2 == friendId) ) && c.FriendStatus == friendStatus)
                .ToList();

            ICollection<Users> friendUsers = [];

            foreach (FriendsList i in friendsList)
            {
                int friend;
                Users currentFriend;

                if (i.User1 == friendId)
                {
                    friend = i.User2;
                } else
                {
                    friend = i.User1;
                }

                currentFriend = _context.Users.Where(c => c.UserId == friend).First();

                friendUsers.Add(currentFriend);
            }

            return friendUsers.OrderBy(c => c.FirstName).Skip(startN).Take((endN - startN)).ToList();
        }

        public bool CreateUser(string FirstName, string LastName, string sub)
        {
            //Checking for existing user will be done with AWS Cognito
            //Assuming account was created:
            Users newProfile = new Users()
            {
                FirstName = FirstName,
                LastName = LastName,
                Sub = sub,
            };

            _context.Users.Add(newProfile);

            return SaveChanges();
        }

        public bool UpdateUser(Users newUser)
        {
            _context.Update(newUser);

            return SaveChanges();
        }

        public bool DeleteUser(string sub)
        {
            var deleteUser = GetUser(sub);
            _context.Users.Remove(deleteUser);
            return SaveChanges();
        }
    }
}
