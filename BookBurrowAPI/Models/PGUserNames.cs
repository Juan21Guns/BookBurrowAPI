using System.Diagnostics.CodeAnalysis;

namespace BookBurrowAPI.Models
{
    public class PGUserNames
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string Username { get; set; } = "";
        public Users? User { get; }
        public PrivateGroups? Chat { get; }
    }
}
