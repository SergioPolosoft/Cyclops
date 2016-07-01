using Application.Payloads;
using ApplicationServices;

namespace QCEvaluation.Application.Events
{
    public class QCResultReceived : IEvent
    {
        public QCResultReceived(QCResultPayload qcResult)
        {
            this.QCResult = qcResult;
        }

        public QCResultPayload QCResult { get; private set; }
    }
}