using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Application.Queries.CandidateTests;
using static TestOnlineProject.API.Dtos.CandateTestRequest;
using TestOnlineProject.Application.Commands.CandidateTests;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/candidatetests")]
    [ApiController]
    public class CandidateTestController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public CandidateTestController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidateTests(CancellationToken cancellationToken)
        {
            var query = new GetAllCandidateTestsQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCandidateTestDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCandidateTestDetailsQuery() { Id = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidateTest([FromBody] CreateCandidateTestRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateCandidateTestCommand()
            {
               TestId = request.testId,
               CandidateId = request.candidateId,
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCandidateTest([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCandidateTestCommand() { Id = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpPost]
        [Route("{id}/mark")]
        public async Task<IActionResult> MarkTheQuestionInTest([FromRoute] Guid id, 
                                            [FromBody] MarkTheAnswerInCandidateTestRequest request, 
                                            CancellationToken cancellationToken)
        {
            var command = new MarkTheAnswerInCandidateTestCommand()
            {
                CandidateTestId = id,
                AnswerId = request.answerId,
                Score = request.score,
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpPost]
        [Route("{id}/submit-answers")]
        public async Task<IActionResult> SubmitAnswer([FromRoute] Guid id,
                                                [FromBody] SubmitAnswerRequest request,
                                                CancellationToken cancellationToken)
        {
            var answerRequests = new List<AnswerData>();
            foreach(var req in request.answers)
            {
                var answer = new AnswerData(req.AnswerId, req.AnswerText);
                answerRequests.Add(answer);
            }

            var command = new SubmitAnswersCommand()
            {
                CandidateTestId = id,
                AnswerDatas = answerRequests,
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }
    }
}
