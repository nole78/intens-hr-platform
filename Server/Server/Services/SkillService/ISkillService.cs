using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.DTOs;

namespace Server.Services.SkillService
{
    public interface ISkillService
    {
        public Task<Result<CreateSkillResponsDto>> AddSkillAsync(CreateSkillDto dto);
    }
}
