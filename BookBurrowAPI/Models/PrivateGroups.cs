using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBurrowAPI.Models
{
    public class PrivateGroups
    {
        [Key]
        public int ChatId { get; set; }
        public int BookId { get; set; }
        public int BookChapter { get; set; }
        public bool IsPrivate { get; set; }
        public required string ChatName { get; set; }
        public int GroupAdmin { get; set; }
        public ICollection<PGUserNames>? Chats { get; set; }
        public ICollection<Messages>? Messages { get; set; }
    }
}
