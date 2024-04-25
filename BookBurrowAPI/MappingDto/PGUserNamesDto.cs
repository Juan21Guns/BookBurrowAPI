using BookBurrowAPI.Models;

namespace BookBurrowAPI.MappingDto
{
    public class PGUserNamesDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public int ChatId { get; set; }
    }
}
