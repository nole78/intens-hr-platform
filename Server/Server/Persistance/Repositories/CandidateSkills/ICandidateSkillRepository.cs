namespace Server.Persistance.Repositories.CandidateSkills
{
    public interface ICandidateSkillRepository
    {
        Task<bool> AddCandidateSkill(int candidateId, int skillId);
        Task<bool> DeleteCandidateSkill(int candidateId, int skillId);
    }
}
