using ApplicationServices;
using QCEvaluation.Application.DTOs;

namespace QCEvaluation.Application.Responses
{
    public class GetEvaluationResponse:IResponse
    {
        public CommandResult Status { get; private set; }
        public EvaluationDTO EvaluationResult { get; private set; }
    }
}