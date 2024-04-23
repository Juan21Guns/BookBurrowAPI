using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookBurrowAPI.MappingDto
{
    public class FriendsListDto
    {
        public int Id { get; set; }
        [Key]
        public int User1 { get; set; }
        [Key]
        public int User2 { get; set; }
        public int FriendStatus { get; set; }
    }
}
