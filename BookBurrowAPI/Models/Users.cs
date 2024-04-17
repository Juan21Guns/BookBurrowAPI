using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookBurrowAPI.Models
{
    public class Users
    {
        public int UserId { get; set; }
        [NotNull]
        public string? FirstName { get; set; }
        [NotNull]
        public string? LastName { get; set; }
    }
}
