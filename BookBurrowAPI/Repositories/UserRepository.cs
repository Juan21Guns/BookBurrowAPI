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

        Users IUserRepository.GetUser(int Id)
        {
            return _context.Users.Where(r => Id == r.UserId)
                .FirstOrDefault();
        }

        ICollection<Users> IUserRepository.GetUserByName(string firstName, string lastName)
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

            return _context.Users.Where(r => (lastName == r.LastName) || (firstName == r.FirstName)).ToList();
        }

        ICollection<Users> IUserRepository.GetAllUsers(int startN, int endN, int friendId, int friendStatus)
        {
            var friendsList = _context.FriendsList.Where(c => ( (c.User1 == friendId) || (c.User2 == friendId) ) && c.FriendStatus == friendStatus).ToList();
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

/*        Users CreateUser(string FirstName, string LastName)
        {
            var doesExist = IUserRepository.GetUserByName();
        }*/
    }
}
