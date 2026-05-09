using Server.Domain.Models;

namespace Server.Services.CandidateService
{
    public interface ICandidateService
    {
        // TODO: improve candidate service interface definition Task types and parameters
        public Task<Candidate> AddCandidateAsync(/* TODO: add DTO here */);
        public Task RemoveCandidateAsync(int id);
        public Task<Candidate> GetCandidateAsync(/* TODO: add DTO here */);
        public Task AddSkillToCandidateAsync(int candidateId, int skillId);
        public Task RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}
