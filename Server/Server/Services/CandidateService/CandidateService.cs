using Server.Domain.DTOs;
using Server.Domain.Models;
using Server.Persistance.Repositories.Candidates;

namespace Server.Services.CandidateService
{
    public class CandidateService : ICandidateService
    {
        // TODO: implement candidate service
        private readonly ICandidateRepository _candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public Task<Candidate> AddCandidateAsync(CreateCandidateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Candidate>> GetCandidateAsync(string? name, List<int>? skillIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCandidateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            throw new NotImplementedException();
        }
    }
}
