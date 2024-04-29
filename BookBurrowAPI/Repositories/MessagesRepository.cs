using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;

namespace BookBurrowAPI.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DataContext _context;
        public MessagesRepository(DataContext context)
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

        public ICollection<Messages>? GetGroupMessages(int cid, int start, int end = 10)
        {
            try
            {
                var messages = _context.Messages
                .OrderByDescending(c => c.Id)
                .Where(c => c.ChatId == cid)
                .Skip(start)
                .Take(end - start)
                .ToList();

                return messages;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public int? SendMessage(Messages message)
        {
            try
            {
                _context.Messages.Add(message);
                return SaveChanges() == true ? message.Id : null;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool UpdateMessage(Messages message)
        {
            try
            {
                message.Edited = true;
                message.TimeCreated = DateTime.Now;
                _context.Update(message);
                return SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool DeleteMessage(Messages message)
        {
            try
            {
                _context.Messages.Remove(message);
                return SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
