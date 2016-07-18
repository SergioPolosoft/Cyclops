using Application.Payloads;
using ApplicationServices;

namespace QCConfiguration.Application.Responses
{
    public class GetQualityControlFound : GetQualityControlResponse
    {
        public GetQualityControlFound(QualityControlPayload qualityControlPayload)
        {
            this.QualityControl = qualityControlPayload;
            this.Status = CommandResult.Success;
        }
    }
}