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
        public DbSet<FriendsList> FriendsLists { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<PrivateGroup> PrivateGroups { get; set; }
        public DbSet<PGUserNames> PGUserNames { get; set; }
        public DbSet<Messages> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PGUserNames>()
                .HasNoKey();
            modelBuilder.Entity<FriendsList>()
                .HasOne(c => c.User1)
                .WithOne()
                .HasForeignKey(e => e.Id);
        }

    }
}
