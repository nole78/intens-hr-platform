using Microsoft.EntityFrameworkCore;
using Server.Domain.Models;
using Server.Persistance.Context;

namespace Server.Persistance.Repositories.CandidateSkills
{
    public class CandidateSkillRepository : ICandidateSkillRepository
    {
        private readonly AppDbContext _context;
        public CandidateSkillRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCandidateSkillAsync(int candidateId, int skillId)
        {
            var exists = await _context.CandidateSkills.AnyAsync(cs => cs.CandidateId == candidateId && cs.SkillId == skillId);

            if(exists)
                return false;

            _context.CandidateSkills.Add(new CandidateSkill
            {
                CandidateId = candidateId,
                SkillId = skillId
            });

            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> DeleteCandidateSkillAsync(int candidateId, int skillId)
        {
            var candidateSkill = _context.CandidateSkills.FirstOrDefault(cs => cs.CandidateId == candidateId && cs.SkillId == skillId);
        
            if(candidateSkill == null)
                return false;

            _context.CandidateSkills.Remove(candidateSkill);

            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> ExistsAsync(int candidateId, int skillId)
        {
            return _context.CandidateSkills.AnyAsync(cs => cs.CandidateId == candidateId && cs.SkillId == skillId);
        }
    }
}
