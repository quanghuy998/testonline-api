using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Application.Queries.Submissions;
using static TestOnlineProject.API.Dtos.SubmissionRequest;
using TestOnlineProject.Application.Commands.Submissions;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/submission")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public SubmissionController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubmissions(CancellationToken cancellationToken)
        {
            var query = new GetAllSubmissionsQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubmissionDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetSubmissionDetailsQuery() { Id = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmission([FromBody] CreateSubmissionRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateSubmissionCommand()
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
        public async Task<IActionResult> DeleteSubmission([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSubmissionCommand() { Id = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpPost]
        [Route("{id}/mark")]
        public async Task<IActionResult> MarkTheQuestionInTest([FromRoute] Guid id, 
                                            [FromBody] MarkSubmissionRequest request, 
                                            CancellationToken cancellationToken)
        {
            var command = new MarkSubmissionCommand()
            {
                TestId = id,
                AnswerScoreDatas = request.answerScoreDatas,
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }
    }
}
