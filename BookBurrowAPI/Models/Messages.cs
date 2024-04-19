using System.ComponentModel.DataAnnotations;

namespace BookBurrowAPI.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string MessageContent { get; set; } = "";
        public PrivateGroup? Chat { get; }
        public DateTime TimeCreated { get; set; }
        public Users? UserSent { get; }
    }
}
