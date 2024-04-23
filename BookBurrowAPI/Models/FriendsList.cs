using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBurrowAPI.Models
{
    public class FriendsList
    {
        public int Id { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public DateTime? TimeCreated { get; set; }
        public int FriendStatus { get; set; }
    }
}
