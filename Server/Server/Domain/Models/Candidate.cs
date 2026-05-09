namespace Server.Domain.Models
{
    public class Candidate
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } = DateOnly.MaxValue;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}