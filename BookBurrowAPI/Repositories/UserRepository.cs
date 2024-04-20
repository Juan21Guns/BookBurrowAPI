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
            /*            if (returnId) 
                        {
                            return _context.Users.ToList();
                        } else if (!returnId && Id == null)
                        {
                            return null;
                        }*/

            /*            List<Users> output = [];

                        foreach (string IdTag in Id)
                        {
                            output = _context.Users.Where(r => IdTag == r.FirstName).ToList();
                        }*/
            /*string word = Id.ToString();*/
            Console.WriteLine($"The number is {Id}");
            return _context.Users.Where(r => Id == r.UserId)
                .FirstOrDefault();
        }

        ICollection<Users> IUserRepository.GetAllUsers(int n)
        {
            return _context.Users.Take(n).ToList();
        }
    }
}
