using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Services.SkillService;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        // TODO: add validation services
        ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            // TODO: add dependency injection for validation services
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill([FromBody] CreateSkillDto dto)
        {
            // TODO: improve with Result pattern return type and error handling
            var result = await _skillService.AddSkillAsync(dto);
            if (result == null)
            {
                return BadRequest(new { message = "Couldn't create skill" });
            }
            return Ok(result);
        }
    }
}
