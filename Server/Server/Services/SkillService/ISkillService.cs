using Server.Domain.DTOs;
using Server.Domain.Models;

namespace Server.Services.SkillService
{
    public interface ISkillService
    {
        // TODO: improve skill service interface definition Task types and parameters
        public Task<Skill> AddSkillAsync(CreateSkillDto dto);
    }
}
