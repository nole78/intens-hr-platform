using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Persistance.Repositories.Skills;

namespace Server.Services.SkillService
{
    public class SkillService : ISkillService
    {
        // TODO: Implement the skill service
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public Task<Skill> AddSkillAsync(CreateSkillDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
