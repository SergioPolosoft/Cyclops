using Application.Payloads;
using ApplicationServices;
using QCEvaluation.Domain;

namespace QCEvaluation.Application.Events
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