using Microsoft.EntityFrameworkCore;
using Server.Domain.Models;
using Server.Persistance.Context;

namespace Server.Persistance.Repositories.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _context;
        // TODO: Implement methods for managing skills in the database
        public SkillRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Skill> AddSkill(Skill skill)
        {
            var added = await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public  Task<Skill?> GetById(int id)
        {
            return _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill?> GetByName(string name)
        {
            var normalizedName = name.Trim().ToLower();
            return await _context.Skills.FirstOrDefaultAsync(s => s.Name.ToLower() == normalizedName);
        }
    }
}
