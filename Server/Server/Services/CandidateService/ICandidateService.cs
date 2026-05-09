using Server.Domain.DTOs;
using Server.Domain.Models;

namespace Server.Services.CandidateService
{
    public interface ICandidateService
    {
        // TODO: improve candidate service interface definition Task types and parameters
        public Task<Candidate> AddCandidateAsync(CreateCandidateDto dto);
        public Task RemoveCandidateAsync(int id);
        public Task<List<Candidate>> GetCandidateAsync(string? name, List<int>? skillIds);
        public Task AddSkillToCandidateAsync(int candidateId, int skillId);
        public Task RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}
