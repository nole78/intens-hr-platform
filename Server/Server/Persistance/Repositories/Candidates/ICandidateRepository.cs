using Server.Domain.Models;

namespace Server.Persistance.Repositories.Candidates
{
    public interface ICandidateRepository
    {
        Task AddAsync(Candidate candidate);
        Task DeleteAsync(Candidate candidate);
        Task<bool> ExistsAsync(int id);
        Task<Candidate?> GetByIdAsync(int id);
        Task<List<Candidate>> SearchAsync(string? name, List<int>? skillIds);

    }
}
