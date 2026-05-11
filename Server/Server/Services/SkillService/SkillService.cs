using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.DTOs;
using Server.Persistance.Repositories.Skills;
using Server.Common;

namespace Server.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<Result<SkillDto>> AddSkillAsync(CreateSkillDto dto)
        {
            var exists = await _skillRepository.GetByNameAsync(dto.Name);
            if (exists != null)
                return Result<SkillDto>.Failure("Skill with the same name already exists", ErrorType.Validation);
            
            var skill = await _skillRepository.AddSkillAsync(new Skill
            {
                Name = dto.Name
            });     

            if(skill != null)
                return Result<SkillDto>.Success(new SkillDto { Id = skill.Id, Name = skill.Name});
            else
                return Result<SkillDto>.Failure("Couldn't create skill",ErrorType.Internal);
        }
    }
}
