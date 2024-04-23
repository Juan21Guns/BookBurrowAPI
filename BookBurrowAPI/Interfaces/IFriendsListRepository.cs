using BookBurrowAPI.MappingDto;
using BookBurrowAPI.Models;

namespace BookBurrowAPI.Interfaces
{
    public interface IFriendsListRepository
    {
        //0 - pending, 1 - accepted, 2 - denied, 3 - blocked
        //returns true if saved, false if error
        bool SaveChanges();

        int FriendExists(FriendsList friend);

        //use user ints to look up table, if row exists, returns friend status
        FriendsList? FriendConnectionExists(int User1, int User2);

        //from User1's side. Created row in database with timestamp.
        //Sets friendstatus to 0 for pending.
        FriendsList? CreateFriend(int User1, int User2, bool block = false);

        //will authenticate with Cognito to use userId for check
        //from User2's side. Updates time and status. Return status (and possible sns). 
        bool UpdateFriend(FriendsList friend);

        bool BlockFriend(int User1, int User2);

        bool RemoveFriend(FriendsList friend);
    }
}
