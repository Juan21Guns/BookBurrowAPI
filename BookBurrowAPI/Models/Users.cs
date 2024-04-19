using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookBurrowAPI.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [NotNull]
        public string? FirstName { get; set; }
        [NotNull]
        public string? LastName { get; set; }
        public ICollection<PGUserNames>? UserNames { get; set; }
        public ICollection<Messages> Messages { get; set; }
    }
}
