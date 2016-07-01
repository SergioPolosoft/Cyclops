using ApplicationServices;
using QCEvaluation.Domain;

namespace QCConfiguration.Application.Responses
{
    public class GetQualityControlResponse:IResponse
    {
        public CommandResult Status { get; private set; }
        public QualityControlPayload QualityControl { get; private set; }
    }
}