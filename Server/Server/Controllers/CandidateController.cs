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
        // TODO: add validation services
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            // TODO: add dependency injection for validation services
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<ActionResult<Candidate>> AddCandidate(CreateCandidateDto dto)
        {
            var result = await _candidateService.AddCandidateAsync(dto);
            if(result.IsSucces)
            {
                return Ok(result.Value);
            }
            else
            {
                return result.ErrorType switch
                {
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    _ => StatusCode(500, new { message = result.Error })
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCandidate(int id)
        {
            var result = await _candidateService.RemoveCandidateAsync(id);
            if(result.IsSucces)
            {
                return Ok();
            }
            else
            {
                return result.ErrorType switch
                {
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    _ => StatusCode(500, new { message = result.Error })
                };
            }
        }

        [HttpPost("{candidateId}/skills/{skillId}")]
        public async Task<ActionResult> AddSkillToCandidate(int candidateId,int skillId)
        {
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
            if (result.IsSucces)
            {
                return Ok();
            }
            else
            {
                return result.ErrorType switch
                {
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    _ => StatusCode(500, new { message = result.Error })
                };
            }
        }

        [HttpDelete("{candidateId}/skills/{skillId}")]
        public async Task<ActionResult> RemoveSkillFromCandidate(int candidateId, int skillId)
        {
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
            if (result.IsSucces)
            {
                return Ok();
            }
            else
            {
                return result.ErrorType switch
                {
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    _ => StatusCode(500, new { message = result.Error })
                };
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> Search([FromQuery] string? name,[FromQuery] List<int>? skillIds)
        {
            if (string.IsNullOrWhiteSpace(name) && (skillIds == null || skillIds.Count == 0))
            {
                return BadRequest(new { message = "At least one of the parameters must be provided" });
            }
            var result = await _candidateService.GetCandidateAsync(name, skillIds);
            if (result.IsSucces)
            {
                return Ok(result.Value);
            }
            else
            {
                return result.ErrorType switch
                {
                    ErrorType.Validation => BadRequest(new { message = result.Error }),
                    ErrorType.NotFound => NotFound(new { message = result.Error }),
                    _ => StatusCode(500, new { message = result.Error })
                };
            }
            ;        
        }
    }
}
