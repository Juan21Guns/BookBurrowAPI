using BookBurrowAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBurrowAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        
        }

        public DbSet<Users> Users { get; set; }

    }
}
