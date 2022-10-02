using Xunit;
using System;
using System.Collections.Generic;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.UnitTest.DomainTest
{
    public class CandidateTestAggregate
    {
        [Fact]
        public void GivenInformation_WhenCreatingCandidateTest_ThenItShouldBeCreated()
        {
            var testId = Guid.NewGuid();
            var candidateId = Guid.NewGuid();
            var answers = new List<Answer>();

            var candidateTest = new Submission(testId, candidateId, answers);

            Assert.True(testId.Equals(candidateTest.TestId));
            Assert.True(candidateId.Equals(candidateTest.CandidateId));
            Assert.Empty(candidateTest.Answers);
        }

        [Fact]
        public void GivenInformation_WhenSubmitingAnswersInCandidateTest_ThenItShouldBeUpdate()
        {
            var candidateTest = GivenSampleCandidateTest();
            var answerData1 = Guid.Parse("576d4f60-0849-4c05-acc7-08da3bc2aa4f");
            var answerData2 = Guid.Parse("2c625c7e-3811-4da3-4855-08da3bc4041c");
            var answerText1 = "This is an answer no 1";
            var answerText2 = "This is an answer no 2";
            var answerData = new List<AnswerData>()
            {
                new AnswerData(answerData1, answerText1),
                new AnswerData(answerData2, answerText2),
            };

            candidateTest.AddAnswers(answerData);

            Assert.Equal(answerText1, candidateTest.Answers.Find(x => x.Id == answerData1)?.Answer);
            Assert.Equal(answerText2, candidateTest.Answers.Find(x => x.Id == answerData2)?.Answer);
        }

        [Fact]
        public void GivenInformation_WhenMarkingAnswerInCandidateTest_ThenItShouldbeUpdate()
        {
            var candidateTest = GivenSampleCandidateTest();
            var answerData1 = Guid.Parse("576d4f60-0849-4c05-acc7-08da3bc2aa4f");
            var answerData2 = Guid.Parse("2c625c7e-3811-4da3-4855-08da3bc4041c");
            var result1 = 10;
            var result2 = 5;

            candidateTest.MarkTheAnswerInCandidateTest(answerData1, result1);
            candidateTest.MarkTheAnswerInCandidateTest(answerData2, result2);

            Assert.Equal(result1, candidateTest.Answers.Find(x => x.Id == answerData1)?.Score);
            Assert.Equal(result2, candidateTest.Answers.Find(x => x.Id == answerData2)?.Score);
        }

        public Submission GivenSampleCandidateTest()
        {
            var testId = Guid.NewGuid();
            var candidateId = Guid.NewGuid();
            var answer1 = new Answer(Guid.NewGuid());
            answer1.Id = Guid.Parse("576d4f60-0849-4c05-acc7-08da3bc2aa4f");
            var answer2 = new Answer(Guid.NewGuid());
            answer2.Id = Guid.Parse("2c625c7e-3811-4da3-4855-08da3bc4041c");

            var answers = new List<Answer>() { answer1, answer2, };

            return new Submission(testId, candidateId, answers);
        }
    }
}
