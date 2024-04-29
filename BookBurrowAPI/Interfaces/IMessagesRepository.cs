using BookBurrowAPI.Models;

namespace BookBurrowAPI.Interfaces
{
    public interface IMessagesRepository
    {
        bool SaveChanges();
        ICollection<Messages>? GetGroupMessages(int cid, int start, int end = 10);
        int? SendMessage(Messages message);
        bool UpdateMessage(Messages message);
        bool DeleteMessage(Messages message);
    }
}
