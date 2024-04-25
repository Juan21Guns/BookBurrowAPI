namespace BookBurrowAPI.Models
{
    public class UsersPGUserNames
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
