using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TestOnlineProject.Application.Commands.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.UnitTest.ApplicationTest.Commands.CandidateTests
{
    public class SubmitAnswersCommandHandlerTest
    {
        private readonly Mock<ICandidateTestRepository> mockCandidateTestRepository = new();
        private readonly SubmitAnswersCommand command = new()
        {
            CandidateTestId = Guid.NewGuid(),
            AnswerDatas = GivenSampleDatas(),
        };

        [Fact]
        public async Task GivenAnswers_WhenSubmitingAnswersIntoCandidateTest_ThenItShouldReturnSuccess()
        {
            var candidateTest = GivenSampleCandidateTest();
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var handler = new SubmitAnswersCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnswers_WhenSubmitingAnswersIntoCandidateTestDoesNotExist_ThenItShouldReturnErrorMessage()
        {
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new SubmitAnswersCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string mesagge = "The test of candidate does not exist.";
            Assert.Equal(mesagge, result.Message);
        }

        [Fact]
        public async Task GivenAnswers_WhenSubmitingAnswersDoesNotExistInCandidateTest_ThenItShouldReturnErrorMessage()
        {
            var candidateTest = GivenSampleCandidateTest();
            candidateTest.Answers[0].Id = Guid.NewGuid();
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var handler = new SubmitAnswersCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string mesagge = $"The answers with id {command.AnswerDatas[0].AnswerId} does not exist in this test.";
            Assert.Equal(mesagge, result.Message);
        }

        [Fact]
        public async Task GivenAnswers_WhenSubmitingAnswersIntoCandidateTestButItAlreadyHaveAnotherOne_ThenItShouldReturnErrorMessage()
        {
            var answerData = GivenSampleDatas();
            var candidateTest = GivenSampleCandidateTest();
            candidateTest.AddAnswers(answerData);
            mockCandidateTestRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(candidateTest);
            var handler = new SubmitAnswersCommandHandler(mockCandidateTestRepository.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            string mesagge = "Answers is already exists.";
            Assert.Equal(mesagge, result.Message);
        }

        private static List<AnswerData> GivenSampleDatas()
        {
            var answer1 = new AnswerData(Guid.Parse("4a19c312-05a7-4582-6964-08da3bc2e6c5"), "Answer 1");
            answer1.AnswerId = Guid.Parse("b453a29d-6485-40aa-e31d-08da3bc40418");
            var answer2 = new AnswerData(Guid.Parse("b8c46995-6af1-435c-bdbe-502661d891bd"), "Answer 2");
            answer2.AnswerId = Guid.Parse("2c625c7e-3811-4da3-4855-08da3bc4041c");

            return new List<AnswerData>() { answer1, answer2 };
        }

        private Submission GivenSampleCandidateTest()
        {
            var answer1 = new Answer(Guid.Parse("4a19c312-05a7-4582-6964-08da3bc2e6c5"));
            answer1.Id = Guid.Parse("b453a29d-6485-40aa-e31d-08da3bc40418");
            var answer2 = new Answer(Guid.Parse("b8c46995-6af1-435c-bdbe-502661d891bd"));
            answer2.Id = Guid.Parse("2c625c7e-3811-4da3-4855-08da3bc4041c");
            var answers = new List<Answer>() { answer1, answer2 };

            return new Submission(Guid.NewGuid(), Guid.NewGuid(), answers);
        }
    }
}
