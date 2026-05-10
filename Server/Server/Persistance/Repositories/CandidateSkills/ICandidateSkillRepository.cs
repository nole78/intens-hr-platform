using Server.Domain.Models;

namespace Server.Persistance.Repositories.CandidateSkills
{
    public interface ICandidateSkillRepository
    {
        Task<bool> AddCandidateSkillAsync(int candidateId, int skillId);
        Task<bool> DeleteCandidateSkillAsync(int candidateId, int skillId);
        Task<bool> ExistsAsync(int candidateId, int skillId);
    }
}
