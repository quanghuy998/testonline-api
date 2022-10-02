using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Test");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title);
            builder.Property(p => p.IsPublish);
            builder.Property(p => p.ModifiedDate);
            builder.OwnsMany<Question>(p => p.Questions, x =>
            {
                x.ToTable("Question");
                x.HasKey(p => p.Id);

                x.Property(p => p.QuestionText);
                x.Property(p => p.QuestionType);
                x.Property(p => p.Point);
                x.OwnsMany<Choice>(p => p.Choices, y =>
                {
                    y.ToTable("Choice");
                    y.HasKey(p => p.Id);
                    y.WithOwner().HasForeignKey(k => k.QuestionId);

                    y.Property(p => p.ChoiceText);
                    y.Property(p => p.IsCorrect);
                });
            });
        }
    }
}
