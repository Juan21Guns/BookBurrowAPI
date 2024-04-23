namespace BookBurrowAPI.Interfaces
{
    public interface IFriendsListRepository
    {
        //0 - pending, 1 - accepted, 2 - denied, 3 - blocked
        //returns true if saved, false if error
        bool SaveChanges();

        //use user ints to look up table, if row exists, returns friend status
        int FriendConnectionExists(int User1, int User2);
        
        //from User1's side. Created row in database with timestamp.
        //Sets friendstatus to 0 for pending.
/*        int EstablishFriendConnection(int User1, int User2, DateOnly timeCreated);

        //from User2's side. Updates time and status. Return status (and possible sns). 
        int UpdateFriendConnection(int User1, int User2, DateOnly timeCreated, int friendStatus);

        bool RemoveFriendConnection(int User1, int User2);*/
    }
}
