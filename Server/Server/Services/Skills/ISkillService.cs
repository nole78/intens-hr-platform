using Server.Domain.DTOs;
using Server.Common;
using Server.DTOs;

namespace Server.Services.SkillService
{
    public interface ISkillService
    {
        public Task<Result<SkillDto>> AddSkillAsync(CreateSkillDto dto);
    }
}
