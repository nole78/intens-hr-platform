using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Services.SkillService;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill([FromBody] CreateSkillDto dto)
        {
            var result = await _skillService.AddSkillAsync(dto);
            return result.ToActionResult();
        }
    }
}
