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
        // TODO: add validation services
        private readonly ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            // TODO: add dependency injection for validation services
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill([FromBody] CreateSkillDto dto)
        {
            var result = await _skillService.AddSkillAsync(dto);
            if(result.IsSucces)
            {
                return Ok(result.Value);
            }
            else
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    _ => StatusCode(500, new { message = "An error occurred while creating the skill" })
                };
            }
        }
    }
}
