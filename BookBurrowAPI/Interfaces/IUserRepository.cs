using BookBurrowAPI.Models;
using Microsoft.Identity.Client;

namespace BookBurrowAPI.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int Id);
        ICollection<Users> GetAllUsers(int n = 5);
    }
}
