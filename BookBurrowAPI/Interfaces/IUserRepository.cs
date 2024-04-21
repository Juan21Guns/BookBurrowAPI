using BookBurrowAPI.Models;
using Microsoft.Identity.Client;

namespace BookBurrowAPI.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int Id);
        ICollection<Users> GetUserByName(string firstName = null, string lastName = null);
        ICollection<Users> GetAllUsers(int startN, int endN, int friendId, int friendStatus);
/*        Users CreateUser(string FirstName, string LastName);
*/    }
}
