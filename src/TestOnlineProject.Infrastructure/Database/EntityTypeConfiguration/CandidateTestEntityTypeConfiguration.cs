using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class CandidateTestEntityTypeConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("CandidateTest");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TestId);
            builder.Property(p => p.CandidateId);
            builder.Property(p => p.FinishedDate);
            builder.OwnsMany<Answer>(p => p.Answers, x =>
            {
                x.ToTable("Answer");
                x.HasKey(p => p.Id);

                x.Property(p => p.QuestionId);
                x.Property(p => p.Answer);
                x.Property(p => p.Score);
            });
        }
    }
}
