using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookBurrowAPI.Interfaces
{
    public interface IUserRepository
    {
        bool SaveChanges();
        bool UserExists(string sub);
        Users GetUser(string sub);
        ICollection<Users>? GetUserByName(string? firstName = null, string? lastName = null);
        ICollection<Users> GetAllUsers( int endN, string sub, int startN = 0, int friendStatus = 1);
        bool CreateUser( string FirstName, string LastName, string sub);

        bool UpdateUser(Users newUser);
        bool DeleteUser(string sub);
    }
}
