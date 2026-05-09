using System.ComponentModel.DataAnnotations;

namespace Server.Domain.DTOs
{
    public class CreateSkillDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        [StringLength(100,ErrorMessage = "Name to long (max length 100).")]
        public string Name { get; set; } = string.Empty;
    }
}
