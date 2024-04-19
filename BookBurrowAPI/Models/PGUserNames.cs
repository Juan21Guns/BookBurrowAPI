namespace BookBurrowAPI.Models
{
    public class PGUserNames
    {
        public List<Users> UserId { get; } = [];
        public List<PrivateGroup> ChatId { get; } = [];
        public string Username { get; set; } = "";
    }
}
