using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBurrowAPI.Models
{
    public class PrivateGroup
    {
        [Key]
        public int ChatId { get; set; }
        public Books? BookId { get; }
        public int BookChapter { get; set; }
        public ICollection<PGUserNames>? Chats { get; set; }
        public ICollection<Messages>? Messages { get; set; }
    }
}
