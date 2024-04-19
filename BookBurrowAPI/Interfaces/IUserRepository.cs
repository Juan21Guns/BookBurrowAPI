using BookBurrowAPI.Models;
using Microsoft.Identity.Client;

namespace BookBurrowAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<Users> GetUser(bool all = false, string Id = "");

    }
}
