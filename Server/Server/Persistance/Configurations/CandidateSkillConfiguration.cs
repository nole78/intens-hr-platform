using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Domain.Models;
using System.Reflection.Emit;

namespace Server.Persistance.Configurations
{
    public class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateSkill> builder)
        {
            builder.HasKey(cs => new { cs.CandidateId, cs.SkillId });
            
            builder.HasOne(cs => cs.Candidate)
                .WithMany(c => c.CandidateSkills)
                .HasForeignKey(cs => cs.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Skill)
                .WithMany(s => s.CandidateSkills)
                .HasForeignKey(cs => cs.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
            new CandidateSkill
            {
                CandidateId = 1,
                SkillId = 1
            },
            new CandidateSkill
            {
                CandidateId = 1,
                SkillId = 3
            },
            new CandidateSkill
            {
                CandidateId = 1,
                SkillId = 4
            },
            new CandidateSkill
            {
                CandidateId = 2,
                SkillId = 2
            },
            new CandidateSkill
            {
                CandidateId = 2,
                SkillId = 5
            },
            new CandidateSkill
            {
                CandidateId = 2,
                SkillId = 6
            });
        }
    }
}
