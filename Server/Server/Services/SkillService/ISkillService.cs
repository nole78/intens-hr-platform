using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;

namespace Server.Services.SkillService
{
    public interface ISkillService
    {
        public Task<Result<Skill>> AddSkillAsync(CreateSkillDto dto);
    }
}
