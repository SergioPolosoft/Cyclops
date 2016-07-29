using Application.Payloads;

namespace QCRoutine.Application.Ports
{
    public interface IQCEvaluationPort
    {
        void NotifyResultReceived(QCResultPayload qcResultPayload);
    }
}