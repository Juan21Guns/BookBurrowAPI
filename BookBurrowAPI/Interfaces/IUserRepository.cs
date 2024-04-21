using BookBurrowAPI.Models;
using Microsoft.Identity.Client;

namespace BookBurrowAPI.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int Id);
        Users GetUserByName(string name);
        ICollection<Users> GetAllUsers(int startN, int endN, int friendId, int friendStatus);
    }
}
