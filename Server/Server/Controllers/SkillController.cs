using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs;
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
        public async Task<ActionResult<string>> AddSkill([FromBody] CreateSkillDto dto)
        {
            // TODO: implement
            var result = await _skillService.AddSkillAsync(dto);
            return StatusCode(500, new { message = "Not implemented yet" });
        }
    }
}
