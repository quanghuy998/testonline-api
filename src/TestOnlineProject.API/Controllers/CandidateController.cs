using Microsoft.AspNetCore.Mvc;
using TestOnlineProject.Application.Commands.Candidates;
using TestOnlineProject.Application.Queries.Candidates;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;
using static TestOnlineProject.API.Dtos.CandidateRequest;

namespace TestOnlineProject.API.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public CandidateController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates(CancellationToken cancellationToken)
        {
            var query = new GetAllCandidatesQuery();
            var result = await _queryBus.SendAsync(query, cancellationToken);
            
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCandidateDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCandidateDetailsQuery() { Id = id };
            var result = await _queryBus.SendAsync(query, cancellationToken);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateCandidateCommand()
            {
                FirstName = request.firstName,
                LastName = request.lastsName,
                Email = request.email,
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCandidate([FromRoute] Guid id, 
                                            [FromBody] UpdateCandidateRequest request, 
                                            CancellationToken cancellationToken)
        {
            var command = new UpdateCandidateCommand()
            {
                Id = id,
                FirstName = request.firstName,
                LastName = request.lastsName,
                Email = request.email,
            };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCandidateCommand() { Id = id };
            var result = await _commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Response);
        }
    }
}
