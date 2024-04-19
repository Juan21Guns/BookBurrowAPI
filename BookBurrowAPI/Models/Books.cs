using System.ComponentModel.DataAnnotations;

namespace BookBurrowAPI.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string BookTitle { get; set; } = "";
        public string BookDescription { get; set; } = "";
        public string BookImage { get; set; } = "";
        public string BookISBN { get; set; } = "";
    }
}
