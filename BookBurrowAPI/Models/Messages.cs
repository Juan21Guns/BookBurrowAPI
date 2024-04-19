namespace BookBurrowAPI.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public List<PrivateGroup> ChatId { get; } = [];
        public string MessageContent { get; set; } = "";
        public DateTime TimeCreated { get; set; }
        public List<Users> UserSent { get; } = [];
    }
}
