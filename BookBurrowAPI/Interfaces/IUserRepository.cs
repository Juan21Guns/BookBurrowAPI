using BookBurrowAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookBurrowAPI.Interfaces
{
    public interface IUserRepository
    {
        bool SaveChanges();
        bool UserExists(int Id);
        Users GetUser(int Id);
        ICollection<Users>? GetUserByName(string? firstName = null, string? lastName = null);
        ICollection<Users> GetAllUsers( int endN, int friendId, int startN = 0, int friendStatus = 1);
        bool CreateUser( string FirstName, string LastName);

        bool UpdateUser(Users newUser);
        bool DeleteUser(int Id);
    }
}
