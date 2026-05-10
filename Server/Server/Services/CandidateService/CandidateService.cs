using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Persistance.Repositories.Candidates;
using Server.Persistance.Repositories.CandidateSkills;
using Server.Persistance.Repositories.Skills;

namespace Server.Services.CandidateService
{
    public class CandidateService : ICandidateService
    {
        // TODO: implement candidate service
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateSkillRepository _candidateSkillRepository;
        private readonly ISkillRepository _skillRepository;
        public CandidateService(ICandidateRepository candidateRepository,ISkillRepository skillRepository,ICandidateSkillRepository candidateSkillRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateSkillRepository = candidateSkillRepository;
            _skillRepository = skillRepository;
        }
        public async Task<Candidate> AddCandidateAsync(CreateCandidateDto dto)
        {
            // TODO: implement result pattern
            var exists = await _candidateRepository.GetByEmailAsync(dto.Email);
            if (exists != null)
            {
                return new Candidate();
            }
            return await _candidateRepository.AddAsync(new Candidate
            {
                Name = dto.Name,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
            });
        }

        public async Task<bool> AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            // TODO: implement result pattern
            var candidateExists = await _candidateRepository.ExistsAsync(candidateId);
            if (!candidateExists) { 
                return false; 
            }
            var skillExists = await _skillRepository.ExistsAsync(skillId);
            if (!skillExists) {
                return false;
            }
            var connectionExists = await _candidateSkillRepository.ExistsAsync(candidateId, skillId);
            if (connectionExists) {
                return false;
            }

            return await _candidateSkillRepository.AddCandidateSkillAsync(candidateId,skillId);
        }

        public Task<List<Candidate>> GetCandidateAsync(string? name, List<int>? skillIds)
        {
            // TODO: implement result pattern
            return _candidateRepository.SearchAsync(name, skillIds);
        }

        public async Task<bool> RemoveCandidateAsync(int id)
        {
            // TODO: implement result pattern
            var exists = await _candidateRepository.ExistsAsync(id);
            if (!exists)
                return false;

            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
                return false;

            return await _candidateRepository.DeleteAsync(candidate);
        }

        public async Task<bool> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            // TODO: implement result pattern
            var candidateExists = await _candidateRepository.ExistsAsync(candidateId);
            if (!candidateExists)
                return false;

            var skillExists = await _skillRepository.ExistsAsync(skillId);
            if (!skillExists)
                return false;

            var connectionExists = await _candidateSkillRepository.ExistsAsync(candidateId, skillId);
            if (!connectionExists)
                return false;

            return await _candidateSkillRepository.DeleteCandidateSkillAsync(candidateId, skillId);
        }
    }
}
