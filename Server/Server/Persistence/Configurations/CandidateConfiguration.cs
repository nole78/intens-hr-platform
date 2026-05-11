using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Domain.Models;
using System.Reflection.Emit;

namespace Server.Persistance.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Email)
                .IsUnique();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ContactNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.DateOfBirth)
                .IsRequired();

            builder.HasData(
                new Candidate
                {
                    Id = 1,
                    Name = "John Doe",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    ContactNumber = "1234567890",
                    Email = "jd@gmail.com"
                },
                new Candidate
                {
                    Id = 2,
                    Name = "John Smith",
                    DateOfBirth = new DateOnly(1992, 2, 2),
                    ContactNumber = "0987654321",
                    Email = "js@gmail.com"
                }
            );
        }
    }
}
