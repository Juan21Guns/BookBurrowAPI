﻿using System.Diagnostics.CodeAnalysis;

namespace BookBurrowAPI.Models
{
    public class PGUserNames
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public Users? User { get; }
        public PrivateGroup? Chat { get; }
        public string Username { get; set; } = "";
    }
}
