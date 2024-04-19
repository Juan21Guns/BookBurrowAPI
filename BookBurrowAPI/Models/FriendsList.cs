﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBurrowAPI.Models
{
    public class FriendsList
    {
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public Users User1 { get; set; }
        [Key, Column(Order = 2)]
        public Users User2 { get; set; }
        public DateTime TimeCreated { get; set; }
        public int FriendStatus { get; set; }
    }
}
