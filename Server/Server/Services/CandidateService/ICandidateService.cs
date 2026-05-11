using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;

namespace Server.Services.CandidateService
{
    public interface ICandidateService
    {
        public Task<Result<Candidate>> AddCandidateAsync(CreateCandidateDto dto);
        public Task<Result> RemoveCandidateAsync(int id);
        public Task<Result<List<Candidate>>> GetCandidateAsync(string? name, List<int>? skillIds);
        public Task<Result> AddSkillToCandidateAsync(int candidateId, int skillId);
        public Task<Result> RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}
