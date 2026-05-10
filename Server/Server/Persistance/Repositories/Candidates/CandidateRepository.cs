using Microsoft.EntityFrameworkCore;
using Server.Domain.Models;
using Server.Persistance.Context;

namespace Server.Persistance.Repositories.Candidates
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            var added = await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<bool> DeleteAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);

            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Candidates.AnyAsync(c => c.Id == id);
        }

        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            var normalised = email.Trim().ToLower();
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email.ToLower() == normalised);
        }

        public Task<Candidate?> GetByIdAsync(int id)
        {
            return _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Candidate>> SearchAsync(string? name, List<int>? skillIds)
        {
            var query = _context.Candidates
                .AsNoTracking()
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                var normalised = name.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(normalised));
            }

            if(skillIds != null && skillIds.Count > 0)
            {
                query = query.Where(c => c.CandidateSkills.Any(cs => skillIds.Contains(cs.SkillId)));
            }

            return await query.ToListAsync();
        }
    }
}
