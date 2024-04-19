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

        ICollection<Users> IUserRepository.GetUser(bool all, string Id)
        {
            if (all) 
            {
                return _context.Users.ToList();
            }

            List<Users> output = [];

            foreach (int IdTag in Id)
            {
                output = _context.Users.Where(r => IdTag == r.UserId).ToList();
            }

            return output;
        }
    }
}
