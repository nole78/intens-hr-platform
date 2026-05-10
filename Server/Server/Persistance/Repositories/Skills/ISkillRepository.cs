using Server.Domain.Models;

namespace Server.Persistance.Repositories.Skills
{
    public interface ISkillRepository
    {
        // TODO: define methods for managing skills in the database
        public Task<Skill> AddSkill(Skill skill);
        public Task<Skill> GetById(int id);
        public Task<Skill> GetByName(string name);
    }
}
