using Server.Domain.DTOs;
using Server.Domain.Models;

namespace Server.Services.CandidateService
{
    public interface ICandidateService
    {
        // TODO: add Result pattern return types for all methods
        public Task<Candidate> AddCandidateAsync(CreateCandidateDto dto);
        public Task<bool> RemoveCandidateAsync(int id);
        public Task<List<Candidate>> GetCandidateAsync(string? name, List<int>? skillIds);
        public Task<bool> AddSkillToCandidateAsync(int candidateId, int skillId);
        public Task<bool> RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}
