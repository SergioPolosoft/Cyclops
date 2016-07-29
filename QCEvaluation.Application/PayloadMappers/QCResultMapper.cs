using Application.Payloads;
using QCEvaluation.Application.Events;
using QCEvaluation.Domain;

namespace QCEvaluation.Application.PayloadMappers
{
    public class QCResultMapper:IDPOMapper<QCResultPayload,QCResult>
    {
        public QCResult Map(QCResultPayload payloadObject)
        {
            var builder = new QCResultBuilder();
            builder.BuildQCResult()
                .WithId(payloadObject.Id)
                .ForTest(payloadObject.TestCode)
                .WithValue(payloadObject.Value);

            return builder.Object;
        }
    }
}