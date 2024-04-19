using System.ComponentModel.DataAnnotations;

namespace BookBurrowAPI.Models
{
    public class FriendsList
    {
        [Key]
        public int Id { get; set; }
        public int User1 { get; }
        public int User2 { get; }
        public DateTime TimeCreated { get; set; }
        public int FriendStatus { get; set; }
    }
}
