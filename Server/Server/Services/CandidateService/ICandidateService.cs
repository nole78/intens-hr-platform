using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.DTOs;

namespace Server.Services.CandidateService
{
    public interface ICandidateService
    {
        public Task<Result<CandidateDto>> AddCandidateAsync(CreateCandidateDto dto);
        public Task<Result> RemoveCandidateAsync(int id);
        public Task<Result<List<CandidateDto>>> GetCandidateAsync(string? name, List<int>? skillIds);
        public Task<Result> AddSkillToCandidateAsync(int candidateId, int skillId);
        public Task<Result> RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}
