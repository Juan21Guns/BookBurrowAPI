using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using System.Data.Entity;

namespace BookBurrowAPI.Repositories
{
    public class FriendsListRepository : IFriendsListRepository
    {
        private readonly DataContext _context;
        public FriendsListRepository(DataContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            try
            {
                int didSave = _context.SaveChanges();
                return didSave > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public int FriendConnectionExists(int User1, int User2)
        {
            try
            {
                var row = _context.FriendsList.Where(c => (c.User1 == User1) && (c.User2 == User2)).First();
                return row.FriendStatus;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
