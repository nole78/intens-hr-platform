using System.ComponentModel.DataAnnotations;

namespace Server.Domain.DTOs
{
    public class CreateSkillDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;
    }
}
