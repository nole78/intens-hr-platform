namespace Server.Domain.Models
{
    public class CandidateSkill
    {
        public int SkillId { get; set; }
        public int CandidateId { get; set; }
        public Skill Skill { get; set; } = null!;
        public Candidate Candidate { get; set; } = null!;
    }
}
