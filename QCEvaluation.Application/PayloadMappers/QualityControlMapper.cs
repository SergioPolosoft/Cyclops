using Application.Payloads;
using QCEvaluation.Domain;

namespace QCEvaluation.Application.PayloadMappers
{
    public class QualityControlMapper:IDPOMapper<QualityControlPayload,QualityControl>
    {
        public QualityControl Map(QualityControlPayload payloadObject)
        {
            return new QualityControl()
            {
                TestCode =  payloadObject.TestCode,
                StandardDeviation =  payloadObject.StandardDeviation,
                TargetValue =  payloadObject.TargetValue
            };
        }
    }
}