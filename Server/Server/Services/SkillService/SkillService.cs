using Server.Common;
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
        public async Task<Result<Skill>> AddSkillAsync(CreateSkillDto dto)
        {
            var exists = await _skillRepository.GetByNameAsync(dto.Name);
            if (exists != null)
                return Result<Skill>.Failure("Skill with the same name already exists", ErrorType.Validation);
            
            var skill = await _skillRepository.AddSkillAsync(new Skill
            {
                Name = dto.Name
            });     

            if(skill != null)
                return Result<Skill>.Success(skill);
            else
                return Result<Skill>.Failure("Couldn't create skill",ErrorType.Internal);
        }
    }
}
