using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Domain.Models;

namespace Server.Persistance.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Name)
                .IsUnique();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Skill
                {
                    Id = 1,
                    Name = "C# programming"
                },
                new Skill
                {
                    Id = 2,
                    Name = "Java programming"
                },
                new Skill
                {
                    Id = 3,
                    Name = "Database design"
                },
                new Skill
                {
                    Id = 4,
                    Name = "English"
                },
                new Skill
                {
                    Id = 5,
                    Name = "Russian"
                },
                new Skill
                {
                    Id = 6,
                    Name = "German language"
                }
            );
        }
    }
}
