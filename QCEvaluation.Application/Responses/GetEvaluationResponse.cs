using ApplicationServices;
using QCEvaluation.Application.DTOs;

namespace QCEvaluation.Application.Responses
{
    public class GetEvaluationResponse:IResponse
    {
        public GetEvaluationResponse(EvaluationDTO evaluation)
        {
            this.EvaluationResult = evaluation;
        }

        public CommandResult Status { get; private set; }
        public string Message { get; private set; }
        public EvaluationDTO EvaluationResult { get; private set; }
    }
}