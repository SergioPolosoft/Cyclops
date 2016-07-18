using Application.Payloads;
using ApplicationServices;

namespace QCEvaluation.Application.Responses
{
    public class GetStandardDeviationRuleByNameResult:IResponse
    {
        public StandardDeviationRulePayload StandardDeviationRule { get; private set; }
        public CommandResult Status { get; private set; }
        public string Message { get; private set; }
    }
}