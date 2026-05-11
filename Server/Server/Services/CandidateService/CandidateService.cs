using Server.Common;
using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.DTOs;
using Server.Persistance.Repositories.Candidates;
using Server.Persistance.Repositories.CandidateSkills;
using Server.Persistance.Repositories.Skills;
using System.Diagnostics.Eventing.Reader;

namespace Server.Services.CandidateService
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateSkillRepository _candidateSkillRepository;
        private readonly ISkillRepository _skillRepository;
        public CandidateService(ICandidateRepository candidateRepository,ISkillRepository skillRepository,ICandidateSkillRepository candidateSkillRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateSkillRepository = candidateSkillRepository;
            _skillRepository = skillRepository;
        }
        public async Task<Result<CandidateDto>> AddCandidateAsync(CreateCandidateDto dto)
        {
            var exists = await _candidateRepository.GetByEmailAsync(dto.Email);
            if (exists != null)
            {
                return Result<CandidateDto>.Failure("Candidate with the same email already exists", ErrorType.Validation);
            }

            var result = await _candidateRepository.AddAsync(new Candidate
            {
                Name = dto.Name,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
            });

            if (result != null)
            {
                return Result<CandidateDto>.Success(new CandidateDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    ContactNumber = result.ContactNumber,
                    Email = result.Email,
                    DateOfBirth = result.DateOfBirth,
                });
            }
            else
            {
                return Result<CandidateDto>.Failure("Couldn't create candidate", ErrorType.Internal);
            }
        }

        public async Task<Result> AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            var candidateExists = await _candidateRepository.ExistsAsync(candidateId);
            if (!candidateExists) 
            { 
                return Result.Failure("Candidate doesn't exist", ErrorType.Validation);
            }

            var skillExists = await _skillRepository.ExistsAsync(skillId);
            if (!skillExists) 
            {
                return Result.Failure("Skill doesn't exist", ErrorType.Validation);
            }

            var connectionExists = await _candidateSkillRepository.ExistsAsync(candidateId, skillId);
            if (connectionExists) 
            {
                return Result.Failure("Candidate already has the skill", ErrorType.Validation);
            }

            var result = await _candidateSkillRepository.AddCandidateSkillAsync(candidateId,skillId);
            
            return result ? Result.Success() : Result.Failure("Couldn't add skill to candidate", ErrorType.Internal);
        }

        public async Task<Result<List<CandidateDto>>> GetCandidateAsync(string? name, List<int>? skillIds)
        {
            var result = await _candidateRepository.SearchAsync(name, skillIds);
            if (result.Count() == 0)
            {
                return Result<List<CandidateDto>>.Failure("No candidates found matching the criteria", ErrorType.NotFound);
            }
            else
            {
                List<CandidateDto> candidates = result.Select(c => new CandidateDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ContactNumber = c.ContactNumber,
                    Email = c.Email,
                    DateOfBirth = c.DateOfBirth,
                    Skills = c.CandidateSkills.Select(cs => new SkillDto
                    {
                        Id = cs.Skill.Id,
                        Name = cs.Skill.Name
                    }).ToList(),

                }).ToList();

                return Result<List<CandidateDto>>.Success(candidates);
            }
        }

        public async Task<Result> RemoveCandidateAsync(int id)
        {
            var exists = await _candidateRepository.ExistsAsync(id);
            if (!exists)
            {
                return Result.Failure("Candidate doesn't exist", ErrorType.Validation);
            }

            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                return Result.Failure("Candidate doesn't exist", ErrorType.Validation);
            }

            var result = await _candidateRepository.DeleteAsync(candidate);

            return result ? Result.Success() : Result.Failure("Couldn't remove candidate", ErrorType.Internal);
        }

        public async Task<Result> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            var candidateExists = await _candidateRepository.ExistsAsync(candidateId);
            if (!candidateExists)
            {
                return Result.Failure("Candidate doesn't exist", ErrorType.Validation);
            }

            var skillExists = await _skillRepository.ExistsAsync(skillId);
            if (!skillExists)
            {
                return Result.Failure("Skill doesn't exist", ErrorType.Validation);
            }

            var connectionExists = await _candidateSkillRepository.ExistsAsync(candidateId, skillId);
            if (!connectionExists)
            {
                return Result.Failure("Candidate doesn't have the skill", ErrorType.Validation);
            }

            var result = await _candidateSkillRepository.DeleteCandidateSkillAsync(candidateId, skillId);

            return result ? Result.Success() : Result.Failure("Couldn't remove skill from candidate", ErrorType.Internal);
        }
    }
}
