using Application.Payloads;
using QCRoutine.Adapters.Service_References.QCEvaluationServiceReference;
using QCRoutine.Application.Ports;

namespace QCRoutine.Adapters
{
    public class QCEvaluationAdapter : IQCEvaluationPort
    {
        public void NotifyResultReceived(QCResultPayload qcResultPayload)
        {
            var client = new QCEvaluationServiceClient();
            client.NotifyResultReceived(new QCResultDTO(){Id = qcResultPayload.Id, TestCode = qcResultPayload.TestCode, Value = qcResultPayload.Value});
        }
    }
}