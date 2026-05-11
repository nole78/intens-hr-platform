using Microsoft.EntityFrameworkCore;
using Server.Domain.Models;
using Server.Persistance.Context;

namespace Server.Persistance.Repositories.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _context;
        public SkillRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var added = await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Skills.AnyAsync(s => s.Id == id);
        }

        public  Task<Skill?> GetByIdAsync(int id)
        {
            return _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill?> GetByNameAsync(string name)
        {
            var normalizedName = name.Trim().ToLower();
            return await _context.Skills.FirstOrDefaultAsync(s => s.Name.ToLower() == normalizedName);
        }
    }
}
