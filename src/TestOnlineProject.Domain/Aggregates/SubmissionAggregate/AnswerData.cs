using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnlineProject.Domain.Aggregates.SubmissionAggregate
{
    public class AnswerData
    {
        public Guid AnswerId { get; set; }
        public string AnswerText { get; }

        public AnswerData(Guid answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }
    }
}
