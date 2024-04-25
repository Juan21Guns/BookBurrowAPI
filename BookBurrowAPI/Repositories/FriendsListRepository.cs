using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.Intrinsics.X86;

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

        public int FriendExists(FriendsList friend)
        {
            try
            {
                return _context.FriendsList.Contains(friend) == true ? 1 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
        }

        public FriendsList? FriendConnectionExists(int User1, int User2)
        {
            try
            {
                var row = _context.FriendsList.Where(c => ((c.User1 == User1) && (c.User2 == User2)) || ((c.User2 == User1) && (c.User1 == User2))).First();
                return row;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public FriendsList? CreateFriend(int User1, int User2, bool block)
        {
            var result = FriendConnectionExists(User1, User2);
            if (result != null)
            {
                return result;
            }

            try
            {
                int fs = 0;

                if (block)
                {
                    fs = 3;
                }

                FriendsList newFriend = new FriendsList()
                {
                    User1 = User1,
                    User2 = User2,
                    TimeCreated = DateTime.Now,
                    FriendStatus = fs
                };
                _context.Add(newFriend);
                return SaveChanges() ? newFriend : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool UpdateFriend(FriendsList friend)
        {
            if (FriendExists(friend) == 0)
            {
                Console.WriteLine("you dont have that friend");
                return false;
            }

            Console.WriteLine("MADE IT THIS FAR");

            if (friend.FriendStatus != 1 && friend.FriendStatus != 2)
            {
                return false;
            }

            Console.WriteLine("MADE IT THIS FAR");
            friend.TimeCreated = DateTime.Now;
            
            _context.Update(friend);

            Console.WriteLine("MADE IT THIS FAR");
            return SaveChanges();
        }

        public bool BlockFriend(int User1, int User2)
        {
            var result = CreateFriend(User1, User2, true);
            Console.WriteLine(result);
            if (result != null)
            {
                int Id = result.Id;
                _context.Remove(result);
                SaveChanges();
                FriendsList newFriend = new FriendsList()
                {
                    Id = Id,
                    User1 = User1,
                    User2 = User2,
                    TimeCreated = DateTime.Now,
                    FriendStatus = 3
                };
                _context.Add(newFriend);
                return SaveChanges();
            }

            return false;
        }

        public bool RemoveFriend(FriendsList friend)
        {
            var connectId = FriendConnectionExists(friend.User1, friend.User2);

            if (connectId == null || connectId.Id != friend.Id)
            {
                return false;
            }

            var deleteId = _context.FriendsList.Where(c => c.Id == friend.Id).First();
            _context.Remove(deleteId);
            return SaveChanges();
        }
    }
}
