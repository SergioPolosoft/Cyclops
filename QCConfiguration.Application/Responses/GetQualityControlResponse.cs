using Application.Payloads;
using ApplicationServices;

namespace QCConfiguration.Application.Responses
{
    public class GetQualityControlResponse:IResponse
    {
        public CommandResult Status { get; internal set; }
        public string Message { get; internal set; }
        public QualityControlPayload QualityControl { get; internal set; }
    }
}