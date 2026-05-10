using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Persistance.Repositories.Skills;

namespace Server.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<Skill> AddSkillAsync(CreateSkillDto dto)
        {
            // TODO: implement Result pattern
            var exists = await _skillRepository.GetByName(dto.Name);
            if (exists.Id != 0)
            {
                throw new Exception("Skill already exists");
            }
            return await _skillRepository.AddSkill(new Skill
            {
                Name = dto.Name
            });     
        }
    }
}
