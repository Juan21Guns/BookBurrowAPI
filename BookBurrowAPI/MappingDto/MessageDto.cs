using BookBurrowAPI.Models;

namespace BookBurrowAPI.MappingDto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string MessageContent { get; set; } = "";
        public DateTime TimeCreated { get; set; }
        public bool Edited { get; set; } = false;
    }
}
