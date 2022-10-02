using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidate");
            builder.HasKey(e => e.Id);

            builder.Property(p => p.FirstName);
            builder.Property(p => p.LastName);
            builder.Property(p => p.Email);
        }
    }
}
