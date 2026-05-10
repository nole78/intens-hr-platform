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
            try
            {
                await _context.Skills.AddAsync(skill);
                await _context.SaveChangesAsync();
                return skill;
            }
            catch
            {
                return new Skill();
            }
        }

        public async Task<Skill> GetById(int id)
        {
            try
            {
                return await _context.Skills.FirstAsync(s => s.Id == id);
            }
            catch
            {
                return new Skill();
            }
        }

        public async Task<Skill> GetByName(string name)
        {
            try
            {
                var normalizedName = name.Trim().ToLower();
                return await _context.Skills.FirstAsync(s => s.Name.ToLower() == normalizedName);
            }
            catch
            {
                return new Skill();
            }
        }
    }
}
