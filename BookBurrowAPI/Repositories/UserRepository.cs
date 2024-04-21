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
            Console.WriteLine($"The number is {Id}");
            return _context.Users.Where(r => Id == r.UserId)
                .FirstOrDefault();
        }

        Users IUserRepository.GetUserByName(string name)
        {
            Console.WriteLine($"The name is {name}");
            return _context.Users.Where(r => name == r.FirstName)
                .FirstOrDefault();
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
    }
}
