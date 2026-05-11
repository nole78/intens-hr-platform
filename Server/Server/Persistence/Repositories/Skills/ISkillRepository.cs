using Server.Domain.Models;

namespace Server.Persistance.Repositories.Skills
{
    public interface ISkillRepository
    {
        // TODO: define methods for managing skills in the database
        public Task<Skill> AddSkillAsync(Skill skill);
        public Task<Skill?> GetByIdAsync(int id);
        public Task<Skill?> GetByNameAsync(string name);
        public Task<bool> ExistsAsync(int id);
    }
}
