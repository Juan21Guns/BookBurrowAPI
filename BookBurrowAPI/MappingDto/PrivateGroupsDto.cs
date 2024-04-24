using BookBurrowAPI.Models;

namespace BookBurrowAPI.MappingDto
{
    public class PrivateGroupsDto
    {
        public int ChatId { get; set; }
        public int BookId { get; set; }
        public int BookChapter { get; set; }
        public bool IsPrivate { get; set; }
    }
}
