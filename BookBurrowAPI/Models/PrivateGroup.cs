using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBurrowAPI.Models
{
    public class PrivateGroup
    {
        [Key]
        public int ChatId { get; set; }
        [ForeignKey(BookId)]
        public List<Books> BookId { get; } = [];
        public int BookChapter { get; set; }
    }
}
