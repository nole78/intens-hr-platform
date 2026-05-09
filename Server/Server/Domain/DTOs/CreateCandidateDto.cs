using System.ComponentModel.DataAnnotations;

namespace Server.Domain.DTOs
{
    public class CreateCandidateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name to long (max length 100).")]
        public string Name { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact number is required.")]
        [StringLength(30, ErrorMessage = "Contact number to long (max length 30).")]
        public string ContactNumber { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, ErrorMessage = "Email to long (max length 255)")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date of birth is required.")]
        public DateOnly DateOfBirth { get; set; }
        public List<int> SkillIds { get; set; } = [];
    }
}
