using Server.Domain.Models;

namespace Server.Persistance.Repositories.Candidates
{
    public interface ICandidateRepository
    {
        Task<Candidate> AddAsync(Candidate candidate);
        Task<bool> DeleteAsync(Candidate candidate);
        Task<bool> ExistsAsync(int id);
        Task<Candidate?> GetByIdAsync(int id);
        Task<Candidate?> GetByEmailAsync(string email);
        Task<List<Candidate>> SearchAsync(string? name, List<int>? skillIds);
    }
}
