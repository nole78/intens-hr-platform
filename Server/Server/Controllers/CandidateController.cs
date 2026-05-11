using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Services.CandidateService;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<ActionResult<Candidate>> AddCandidate(CreateCandidateDto dto)
        {
            var result = await _candidateService.AddCandidateAsync(dto);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCandidate(int id)
        {
            var result = await _candidateService.RemoveCandidateAsync(id);
            return result.ToActionResult();
        }

        [HttpPost("{candidateId}/skills/{skillId}")]
        public async Task<ActionResult> AddSkillToCandidate(int candidateId,int skillId)
        {
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
            return result.ToActionResult();
        }

        [HttpDelete("{candidateId}/skills/{skillId}")]
        public async Task<ActionResult> RemoveSkillFromCandidate(int candidateId, int skillId)
        {
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> Search([FromQuery] string? name,[FromQuery] List<int>? skillIds)
        {
            if (string.IsNullOrWhiteSpace(name) && (skillIds == null || skillIds.Count == 0))
            {
                return BadRequest(new { message = "At least one of the parameters must be provided" });
            }
            var result = await _candidateService.GetCandidateAsync(name, skillIds);
            return result.ToActionResult();
        }
    }
}
