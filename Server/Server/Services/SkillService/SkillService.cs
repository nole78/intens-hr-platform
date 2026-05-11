using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.DTOs;
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
        public async Task<Result<CreateSkillResponsDto>> AddSkillAsync(CreateSkillDto dto)
        {
            var exists = await _skillRepository.GetByNameAsync(dto.Name);
            if (exists != null)
                return Result<CreateSkillResponsDto>.Failure("Skill with the same name already exists", ErrorType.Validation);
            
            var skill = await _skillRepository.AddSkillAsync(new Skill
            {
                Name = dto.Name
            });     

            if(skill != null)
                return Result<CreateSkillResponsDto>.Success(new CreateSkillResponsDto { Id = skill.Id, Name = skill.Name});
            else
                return Result<CreateSkillResponsDto>.Failure("Couldn't create skill",ErrorType.Internal);
        }
    }
}
