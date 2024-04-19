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
                .HasKey(k => new { k.UserId, k.ChatId });
            modelBuilder.Entity<PGUserNames>()
                .HasOne(c => c.User)
                .WithMany(c => c.UserNames)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<PGUserNames>()
                .HasOne(c => c.Chat)
                .WithMany(c => c.Chats)
                .HasForeignKey(c => c.ChatId);

            modelBuilder.Entity<Messages>()
                .HasOne(m => m.UserSent)
                .WithMany(c => c.Messages)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<Messages>()
                .HasOne(c => c.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(c => c.ChatId);

/*            modelBuilder.Entity<FriendsList>()
                .HasOne(f => f.User1)
                .WithMany(fl => fl.Friends)
                .HasForeignKey(f => f.User1Id);
            modelBuilder.Entity<FriendsList>()
                .HasOne(f => f.User2)
                .WithMany(fl => fl.Friends)
                .HasForeignKey(f => f.User2Id);*/
        }

    }
}
