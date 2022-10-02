namespace TestOnlineProject.API.Dtos
{
    public class CandidateRequest
    {
        public record CreateCandidateRequest(string firstName, string lastsName, string email);
        public record UpdateCandidateRequest(string firstName, string lastsName, string email);

    }
}
