using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Services.CandidateService;
using System.Threading.Tasks;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        // TODO: add validation services
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            // TODO: add dependency injection for validation services
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddCandidate([FromBody] CreateCandidateDto dto)
        {
            // TODO: improve with Result pattern return type and error handling
            var result = await _candidateService.AddCandidateAsync(dto);
            if(result == null)
            {
                return BadRequest(new { message = "Couldn't create candidate" });
            }
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<string>> AddSkillToCandidate([FromQuery] int candidateId,[FromQuery] int skillId)
        {
            // TODO: improve with Result pattern return type and error handling
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<string> RemoveSkillFromCandidate([FromQuery]int id)
        {
            // TODO: improve with Result pattern return type and error handling
            var result = _candidateService.RemoveCandidateAsync(id);
            return result.Result ? Ok(new { message = "Candidate removed successfully" }) : BadRequest(new { message = "Couldn't remove candidate" });
        }

        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> Search([FromQuery] string? name,[FromQuery] List<int>? skillIds)
        {
            // TODO: improve with Result pattern return type and error handling
            if (string.IsNullOrWhiteSpace(name) && (skillIds == null || skillIds.Count == 0))
            {
                return BadRequest(new { message = "At least one of the parameters must be provided" });
            }
            var result = await _candidateService.GetCandidateAsync(name, skillIds);
            return Ok(result);        
        }
    }
}
